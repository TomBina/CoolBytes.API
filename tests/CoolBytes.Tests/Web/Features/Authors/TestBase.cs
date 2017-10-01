﻿using System;
using System.IO;
using CoolBytes.Core.Factories;
using CoolBytes.Core.Interfaces;
using CoolBytes.Core.Models;
using CoolBytes.Data;
using CoolBytes.WebAPI.Services;
using Microsoft.AspNetCore.Http;
using Moq;

namespace CoolBytes.Tests.Web.Features.Authors
{
    public abstract class TestBase
    {
        protected AppDbContext Context;
        protected Fixture Fixture;
        protected IUserService UserService;

        protected TestBase(Fixture fixture)
        {
            Fixture = fixture;
            Context = fixture.CreateNewContext();
        }

        protected void InitUserService(User user)
        {
            var userService = new Mock<IUserService>();
            userService.Setup(exp => exp.GetUser()).ReturnsAsync(user);
            UserService = userService.Object;
        }

        protected void InitUserService()
        {
            var user = new User("Test");

            InitUserService(user);
        }

        protected PhotoFactory CreatePhotoFactory()
        {
            var options = new PhotoFactoryOptions(Fixture.TempDirectory);
            var validator = new PhotoFactoryValidator();
            var photoFactory = new PhotoFactory(options, validator);
            return photoFactory;
        }

        protected Mock<IFormFile> CreateFileMock()
        {
            var mock = new Mock<IFormFile>();
            mock.Setup(e => e.FileName).Returns("testimage.png");
            mock.Setup(e => e.ContentType).Returns("image/png");
            mock.Setup(e => e.OpenReadStream()).Returns(() => File.Open("assets/testimage.png", FileMode.Open));
            return mock;
        }
    }
}