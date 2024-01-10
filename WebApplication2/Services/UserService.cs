using Microsoft.EntityFrameworkCore;
using MrezeBackend.DTOs.AuthDTOs;
using MrezeBackend.Entities;

namespace MrezeBackend.Services
{
    public class UserService
    {
        private readonly ApplicationDbContext _context;
        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ClientResponseDTO?> SaveUser(ClientRegisterBodyDTO clientRegisterBodyDTO)
        {
            var newClient = new User
            {
                Email = clientRegisterBodyDTO.Email,
                Password = clientRegisterBodyDTO.Password,
                Name = clientRegisterBodyDTO.Name
            };
            _context.Users.Add(newClient);
            await _context.SaveChangesAsync();
            return new ClientResponseDTO
            {
                Email = newClient.Email,
                Name = newClient.Name
            };
        }

        public async Task<User?> GetUser(string email)
        {
            User user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                return null;
            }
            return user;
        }
    }
}
