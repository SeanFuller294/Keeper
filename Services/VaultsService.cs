using System;
using System.Collections.Generic;
using Keeper.Models;
using Keeper.Repositories;

namespace Keeper.Services
{
  public class VaultsService
  {
    private readonly VaultsRepository _repo;
    public VaultsService(VaultsRepository repo)
    {
      _repo = repo;
    }
    public IEnumerable<Vault> Get(string uId)
    {
      return _repo.Get(uId);
    }

    public Vault GetById(int id)
    {
      Vault exists = _repo.Get(id);
      if (exists == null) { throw new Exception("Invalid id"); }
      return exists;
    }

    public Vault Create(Vault newVault)
    {
      int id = _repo.Create(newVault);
      newVault.id = id;
      return newVault;
    }

    public string Delete(int id, string userId)
    {
      Vault v = _repo.Get(id);
      if (v == null || v.userId != userId) { throw new Exception("No"); }
      _repo.Delete(id);
      return "Success";
    }
  }
}