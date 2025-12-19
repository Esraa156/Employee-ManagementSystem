
using BCrypt.Net;
using ApplicationLayer.DTOs;
using ApplicationLayer.Interfaces;
using ApplicationLayer;

namespace Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenGenerator _tokenGenerator;

        public AuthService(IUserRepository userRepository, IJwtTokenGenerator tokenGenerator)
        {
            _userRepository = userRepository;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<AuthResultDTO> LoginAsync(LoginDto dto)
        {
            var user = await _userRepository.GetByEmailAsync(dto.Email);

            if (user == null || !user.IsActive)
            {
                return new AuthResultDTO
                {
                    Success = false,
                    Message = "Email or password is incorrect"
                };
            }

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);

            if (!isPasswordValid)
            {
                return new AuthResultDTO
                {
                    Success = false,
                    Message = "Invalid password"
                };
            }

            // Generate JWT Token
            var token = _tokenGenerator.GenerateToken(user);

            return new AuthResultDTO
            {
                Success = true,
                Token = token,
                FullName = user.FullName,
                Role = user.Role
            };
        }

     
    }
}
