using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebDatingApp.API.Domain.Models;

namespace WebDatingApp.API.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;
        public AuthRepository(DataContext context)
        {
            _context = context;

        }

        public async Task<User> Login(string username, string password)
        {
            // se usarmos FirstOrDefaultAsync, caso ele n encontre ele retorna null
            // se usassemos FirstAsync, caso n encontre ele retorna uma Exception
            // utiliza Include para retornar o Photos, senao todos os atributos seriam null para photos
            var user = await _context.Users.Include(p => p.Photos).FirstOrDefaultAsync(x => x.Username == username);

            if (user == null) 
                return null;

            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            return user;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt)) 
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++) {
                    if(computedHash[i] != passwordHash[i])
                        return false;
                }
            }
            return true;
        }

        public async Task<User> Register(User user, string password)
        {
            byte[] passwordHash, passwordSalt;
            // Passamos a referencia da variavel utilizando 'out', assim, quando for atualizado o valor dela no método
            // aqui onde foi declarado o valor tbm será atualizado
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            // utilizamos o using para declarar a variavel e garantir a mesma instancia
            using(var hmac = new System.Security.Cryptography.HMACSHA512()) 
            {
                passwordSalt = hmac.Key;
                // temos que passar password como byte[], entao usamos enconding
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
            
        }

        public async Task<bool> UserExists(string username)
        {
            var userExists = await _context.Users.AnyAsync(x => x.Username == username);

            if(!userExists)
                return false;

            return true;
        }
    }
}