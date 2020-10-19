﻿using System;
using ItHappened.Domain;
using Serilog;

namespace ItHappened.Application.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Guid CreateUser(string name)
        {
            var user = new User(Guid.NewGuid(), name, DateTimeOffset.UtcNow);
            _userRepository.SaveUser(user);
            Log.Verbose($"User with login {name} added with id={user.Guid}");
            return user.Guid;
        }

        public User GetUser(Guid id)
        {
            return _userRepository.LoadUser(id);
        }
    }
}