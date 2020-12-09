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
    public class ProjectCrud
    {

        //logger
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        //DB Initialazation, with input saved in launchSettings.json
        private readonly IMongoCollection<ProjectModel> _project;
        public ProjectCrud(IProjectDBSettings settings)
        {
            try
            {
                var client = new MongoClient(settings.ConnectionString);
                var database = client.GetDatabase(settings.Database);
                _project = database.GetCollection<ProjectModel>(settings.Collection);
            }
            catch (Exception e)
            {
                logger.Error($"Error Code 2.1 - Database connection establishment\n{e.Message}");
                throw new Exception("Error Code 2.1 - Database connection establishment");

            }
        }

        //Get All User Login info
        public List<ProjectModel> Get() =>
           Task.Run(() => _project.Find(x => true).ToList()).Result;

        //Get a Specific user Project by Id 
        public ProjectModel GetProjectById(ObjectId id) =>
           Task.Run(() => _project.Find(x => x.Id == id).FirstOrDefault()).Result;

        //Add new Project to Project DB
        public void Create(ProjectModel content) =>
            Task.Run(() => _project.InsertOne(content));

        //Update exising Project
        public void Update(ObjectId id, ProjectModel updatedUser) =>
           Task.Run(() => _project.ReplaceOne(x => x.Id == id, updatedUser));

        //Delete existing Project
        public void Delete(ObjectId id) =>
           Task.Run(() => _project.DeleteOne(x => x.Id == id));
    }
}