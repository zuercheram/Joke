using Joke.Server.Models.Entities;
using Microsoft.Data.SqlClient;

namespace Joke.Server.Repositories;

public class JokeRepository
{
    private readonly string _connectionString;

    public JokeRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetSection("ConnectionStrings")
            .GetValue<string>("DefaultConnection");
    }

    public async Task<int> SaveJokeAsync(JokeEntity joke)
    {
        using SqlConnection connection = new(_connectionString);
        connection.Open();

        var sqlCommand = connection.CreateCommand();

        sqlCommand.CommandText = @"INSERT INTO Jokes (SourceId, Value, Url, IconUrl, CreatedAt, UpdatedAt)
            VALUES (@SourceId, @Value, @Url, @IconUrl, @CreatedAt, @UpdatedAt)";

        sqlCommand.Parameters.AddWithValue("@SourceId", joke.SourceId);
        sqlCommand.Parameters.AddWithValue("@Value", joke.Value);
        sqlCommand.Parameters.AddWithValue("@Url", joke.Url);
        sqlCommand.Parameters.AddWithValue("@IconUrl", joke.IconUrl);
        sqlCommand.Parameters.AddWithValue("@CreatedAt", joke.CreatedAt);
        sqlCommand.Parameters.AddWithValue("@UpdatedAt", joke.UpdatedAt);

        return await sqlCommand.ExecuteNonQueryAsync();
    }

    public async Task<List<JokeEntity>> GetJokesAsync()
    {
        var jokeList = new List<JokeEntity>();

        using SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();

        var sqlCommand = connection.CreateCommand();

        sqlCommand.CommandText = @"SELECT * FROM Jokes";
        using SqlDataReader dbResult = sqlCommand.ExecuteReader();
        while (dbResult.Read())
        {
            jokeList.Add(CreateEntity(dbResult));
        }
        return jokeList;
    }

    public async Task<JokeEntity?> GetJokeAsync(int id)
    {
        using SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();

        var sqlCommand = connection.CreateCommand();

        sqlCommand.CommandText = @"SELECT * FROM Jokes WHERE ID = @Id";
        sqlCommand.Parameters.AddWithValue("@Id", id);
        using SqlDataReader dbResult = sqlCommand.ExecuteReader();
        if (dbResult.Read())
        {
            return CreateEntity(dbResult);
        }
        return null;
    }

    private static JokeEntity CreateEntity(SqlDataReader dbResult)
    {
        return new JokeEntity
        {
            Id = Convert.ToInt32(dbResult["Id"]),
            SourceId = Convert.ToString(dbResult["SourceId"]),
            Value = Convert.ToString(dbResult["Value"]),
            Url = Convert.ToString(dbResult["Url"]),
            IconUrl = Convert.ToString(dbResult["IconUrl"]),
            CreatedAt = Convert.ToDateTime(dbResult["CreatedAt"]),
            UpdatedAt = Convert.ToDateTime(dbResult["UpdatedAt"])
        };
    }
}
