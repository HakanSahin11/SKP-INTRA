using MongoDB.Bson;
using MongoDB.Driver;
using NLog;
using SKP_IntranetSideAPI.DB_Settings;
using SKP_IntranetSideAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SKP_IntranetSideAPI.Cruds
{
    public class LoginCrud
    {
        //logger
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        //DB Initialazation, with input saved in launchSettings.json
        private readonly IMongoCollection<LoginModel> _login;
        public LoginCrud(ILoginDBSettings settings)
        {
            try
            {
                var client = new MongoClient(settings.ConnectionString);
                var database = client.GetDatabase(settings.Database);
                _login = database.GetCollection<LoginModel>(settings.Collection);
            }
            catch(Exception e)
            {
                logger.Error($"Error Code 1.1 - Database connection establishment\n{e.Message}");
                throw new Exception("Error Code 1.1 - Database connection establishment");
            }
        }

        //Get All User Login info
        public List<LoginModel> Get() =>
           Task.Run(() => _login.Find(x => true).ToList()).Result;

        //Get Specific user by username (for login)
        public LoginModel GetUser(string Email) =>
            Task.Run(() => _login.Find(x => x.Email == Email).FirstOrDefault()).Result;


        //Get Specific user by username (for login)
        public LoginModel GetUserByUserName(string userName) =>
            Task.Run(() => _login.Find(x => x.UserName == userName).FirstOrDefault()).Result;

        //Get a Specific user login by Id 
        public LoginModel GetLoginById(ObjectId id) =>
           Task.Run(() => _login.Find(x => x.Id == id).FirstOrDefault()).Result;

        //Add new user login to UserLogin DB
        public void Create(LoginModel content) =>
            Task.Run(() => _login.InsertOne(content));

        //Update exising Login
        public void Update(string username, LoginModel updatedUser) =>
           Task.Run(() => _login.ReplaceOne(x => x.UserName == username, updatedUser));

        //Delete existing Login
        public void Delete(ObjectId id) =>
           Task.Run(() => _login.DeleteOne(x => x.Id == id));
    }
}
