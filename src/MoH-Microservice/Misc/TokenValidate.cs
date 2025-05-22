using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MoH_Microservice.Data;
using MoH_Microservice.Models;
using NuGet.Protocol;
using System.IdentityModel.Tokens.Jwt;

namespace MoH_Microservice.Misc
{
    public class TokenValidate 
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly JwtSecurityTokenHandler _jwt;
        private string _token;
        public TokenValidate(UserManager<AppUser> context) 
        {
            this._jwt= new JwtSecurityTokenHandler();
            this._userManager = context;
        }
        public TokenValidate setToken(string token)
        {
            if(token==null)
                throw new ArgumentNullException("token:  invalid argument");

            this._token = token;
            return this;
        }
        public string getToken()
        {
            if (this._token == null)
                throw new NullReferenceException("token : user is invalid");
            return this._token;
        }
        public string getUserName()
        {
            var handler = this._jwt.ReadJwtToken(this.getToken());
            var username = handler.Payload.Where(w => w.Key == "name").Select(s => s.Value).First().ToString();
            if (username==null)
                throw new NullReferenceException("token: username");
            return username; 
        }
        public async Task<AppUser> db_recorded()
        {
            var user= await this._userManager.FindByNameAsync(this.getUserName());
            if(user==null)
                throw new NullReferenceException("token: user does not exist");
            return user;
        }
    }
}
