using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using Keeper.Models;
using Microsoft.AspNetCore.Http;

namespace Keeper.Repositories
{
  public class VaultKeepsRepository
  {
    private readonly IDbConnection _db;
    public VaultKeepsRepository(IDbConnection db)
    {
      _db = db;
    }
    internal int Create(VaultKeep newVK)
    {
      string sql = "INSERT INTO vaultkeeps(userId, keepId, vaultId) VALUES(@userId, @keepId, @vaultId); SELECT LAST_INSERT_ID()";
      return _db.Execute(sql, newVK);
    }

    internal VaultKeep GetOne(int id)
    {
      string sql = "SELECT * FROM vaultkeeps WHERE id=@id";
      return _db.QueryFirstOrDefault(sql, new { id });
    }

    internal void Delete(VaultKeep vk)
    {
      string sql = "DELETE FROM vaultkeeps WHERE vaultId = @vaultId AND keepId = @keepId";
      _db.Execute(sql, vk);
    }
    internal object Get(int vaultId)
    {
      string sql = "SELECT * FROM vaultkeeps WHERE vaultId = @vaultId";
      return _db.Query(sql, new { vaultId });
    }
  }
}