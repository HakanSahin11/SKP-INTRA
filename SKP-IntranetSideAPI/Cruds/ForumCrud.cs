using MongoDB.Bson;
using MongoDB.Driver;
using NLog;
using SKP_IntranetSideAPI.DB_Settings;
using SKP_IntranetSideAPI.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SKP_IntranetSideAPI.Cruds
{
    public class ForumCrud
    {
        //logger
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        //DB Initialazation, with input saved in launchSettings.json
        private readonly IMongoCollection<ForumModel> _content;
        public ForumCrud(IForumDBSettings settings)
        {
            try
            {
                var client = new MongoClient(settings.ConnectionString);
                var database = client.GetDatabase(settings.Database);
                _content = database.GetCollection<ForumModel>(settings.Collection);
            }
            catch (Exception e)
            {
                logger.Error($"Error Code 4.1 - Database connection establishment\n{e.Message}");
                throw new Exception("Error Code 4.1 - Database connection establishment");

            }
        }

        //Get all users
        public async Task<List<ForumModel>> Get() =>
            await Task.Run(() =>
            _content.Find(x => true).ToList());

        //Get specific user by Id
        public async Task<ForumModel> GetUserById(ObjectId id) =>
           await Task.Run(() =>
           _content.Find(x => x.Id == id).FirstOrDefault());

        //Create new user
        public async Task<ForumModel> Create(ForumModel user)
        {
            await Task.Run(() =>
            _content.InsertOne(user));
            return user;
        }

        //Update existing user
        public async void Update(ObjectId id, ForumModel updatedUser) =>
           await Task.Run(() =>
           _content.ReplaceOne(x => x.Id == id, updatedUser));

        //Delete existing user
        public async void Delete(ObjectId id) =>
           await Task.Run(() =>
           _content.DeleteOne(x => x.Id == id));

    }
}

