using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using Keeper.Models;
using Microsoft.AspNetCore.Mvc;

namespace Keeper.Repositories
{
  public class KeepsRepository
  {
    private readonly IDbConnection _db;
    public KeepsRepository(IDbConnection db)
    {
      _db = db;
    }
    internal IEnumerable<Keep> Get()
    {
      string sql = "SELECT * FROM keeps";
      return _db.Query<Keep>(sql);
    }

    internal Keep Get(int id)
    {
      string sql = "SELECT * FROM keeps WHERE id = @id";
      return _db.QueryFirstOrDefault<Keep>(sql, new { id });
    }

    internal void Edit(Keep newKeep)
    {
      string sql = "UPDATE keeps SET name = @name, hasBeenKept = @hasBeenKept, shares = @shares, views = @views, description = @Description, img = @Img, isprivate = @IsPrivate WHERE id = @id";
      _db.Execute(sql, newKeep);
    }

    internal IEnumerable<Keep> GetKeepsByUser(string id)
    {
      string sql = "SELECT * FROM keeps WHERE userId = @id";
      return _db.Query<Keep>(sql, new { id });
    }

    internal int Create(Keep newKeep)
    {
      string sql = "INSERT INTO keeps(name, description, userId, img, isPrivate) VALUES (@name, @description, @userId, @img, @isPrivate); SELECT LAST_INSERT_ID();";
      return _db.Execute(sql, newKeep);
    }

    internal void Delete(int id)
    {
      string sql = "DELETE FROM keeps WHERE id = @id";
      _db.Execute(sql, new { id });
    }
  }
}