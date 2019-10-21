using System;
using System.Collections.Generic;
using Keeper.Models;
using Keeper.Repositories;
using Microsoft.AspNetCore.Http;

namespace Keeper.Services
{
  public class VaultKeepsService
  {
    private readonly VaultKeepsRepository _repo;
    public VaultKeepsService(VaultKeepsRepository repo)
    {
      _repo = repo;
    }
    internal object Create(VaultKeep newVK)
    {
      newVK.id = _repo.Create(newVK);
      return newVK;
    }

    internal string Delete(VaultKeep vk, string userId)
    {
      if (vk == null || vk.userId != userId) { throw new Exception("Invalid Id"); }
      _repo.Delete(vk);
      return "Success";
    }

    internal object Get(int id)
    {
      return _repo.Get(id);
    }
  }
}