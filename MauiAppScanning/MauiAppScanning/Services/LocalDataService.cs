using LiteDB;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.FastCrud;
using Z.Dapper.Plus;
using System.Linq.Expressions;

namespace MauiAppScanning.Services;

/// <summary>
/// POC Version of local data service. Need to factor out the interface and implement a repository pattern.
/// </summary>
public class LocalDataService
{
    private LiteDatabase db; //TODO: Implement IDisposable
    private readonly string dbPath = Path.Combine(FileSystem.AppDataDirectory, "Storm.db");

    public LocalDataService()
    {
        db = new LiteDatabase(dbPath);
    }

    //TODO: Should collections be exposed as strong types?

    public T FindOne<T>(Expression<Func<T, bool>> predicate)
    {
        return db.GetCollection<T>().FindOne(predicate);
    }

    public IEnumerable<T> FindAll<T>()
    {
        return db.GetCollection<T>().FindAll();
    }

    public IEnumerable<T> Find<T>(Expression<Func<T, bool>> predicate)
    {
        return db.GetCollection<T>().Find(predicate);
    }

    public void Insert<T>(T entity)
    {
        db.GetCollection<T>().Insert(entity);
        //TODO: Index
    }

    public void Update<T>(T entity)
    {
        db.GetCollection<T>().Update(entity);
    }

    public void DeleteAll<T>()
    {
        db.GetCollection<T>().DeleteAll();
    }

    #region
    //TODO: Get from settings file.
    private readonly string connectionString = "YOUR CONNECTION STRING HERE!";
    #endregion


    /// <summary>
    /// POC version of cloud sync. This will be replaced with a more robust solution.
    /// </summary>
    public async Task SyncFromCloud()
    {
        using var connection = new SqlConnection(connectionString);
        connection.Open();
        var crew = await connection.FindAsync<CrewMember>(statement => statement.Include<Vendor>());

        File.Delete(dbPath);
        db = new LiteDatabase(dbPath);
        var crewCollection = db.GetCollection<CrewMember>();
        crewCollection.InsertBulk(crew);
        crewCollection.EnsureIndex(c => c.LastName);
    }

    public async Task SyncToCloud()
    {
        using var connection = new SqlConnection(connectionString);
        connection.Open();
        var crew = db.GetCollection<CrewMember>().FindAll();

        //connection.BulkUpdate(crew, crew => crew.Registrations);

        foreach (var member in crew)
        {
            var existing = await connection.FindAsync<CrewMember>(statement => statement.Where($"CrewMemberId = @Id").WithParameters(new { member.Id }));
            if (existing.Any())
            {
                bool success = await connection.UpdateAsync(member);
                Debug.WriteLine($"Update success {success}");
            }
            else
            {
                await connection.InsertAsync(member);
            }
        }
    }
}
