using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using Keeper.Models;
using Microsoft.AspNetCore.Mvc;

namespace Keeper.Repositories
{
  public class VaultsRepository
  {
    private readonly IDbConnection _db;
    public VaultsRepository(IDbConnection db)
    {
      _db = db;
    }
    internal IEnumerable<Vault> Get(string uId)
    {

      string sql = "SELECT * FROM vaults WHERE userId = @uId";
      return _db.Query<Vault>(sql, new { uId });
    }

    internal Vault Get(int id)
    {
      string sql = "SELECT * FROM vaults WHERE id = @id";
      return _db.QueryFirstOrDefault<Vault>(sql, new { id });
    }

    internal int Create(Vault newVault)
    {
      string sql = "INSERT INTO vaults(name,description, userId) VALUES(@name, @description, @userId); SELECT LAST_INSERT_ID()";
      return _db.Execute(sql, newVault);
    }

    internal void Delete(int id)
    {
      string sql = "DELETE FROM vaults WHERE id = @id";
      _db.Execute(sql, new { id });
    }
  }
}