using System.Collections.Generic;
using System.Threading.Tasks;
using WebDatingApp.API.Domain.Models;
using WebDatingApp.API.Helpers;

namespace WebDatingApp.API.Data
{
    public interface IDatingRepository
    {
        // The type could be an user or photo
         void Add<T>(T entity) where T: class;
         void Delete<T>(T entity) where T: class;
         Task<bool> SaveAll();
         Task<PagedList<User>> GetUsers(UsersParams usersParams);
         Task<User> GetUser(int id);
         Task<Photo> GetPhoto(int id);
         Task<Photo> GetMainPhotoFromUser(int userId);
    }
}