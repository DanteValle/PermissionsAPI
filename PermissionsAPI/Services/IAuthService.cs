﻿namespace PermissionsAPI.Services
{
    public interface IAuthService
    {
        string GenerateToken(string username);
    }
}