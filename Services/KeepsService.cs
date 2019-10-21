using System;
using Keeper.Models;
using Keeper.Repositories;
using System.Collections.Generic;
using Keepr.Models;

namespace Keeper.Services
{
  public class KeepsService
  {
    private readonly KeepsRepository _repo;
    public KeepsService(KeepsRepository repo)
    {
      _repo = repo;
    }
    public IEnumerable<Keep> Get()
    {
      return _repo.Get();
    }

    public Keep Get(int id)
    {
      Keep exists = _repo.Get(id);
      if (exists == null) { throw new Exception("Invalid Id, Yo."); }
      return exists;
    }
    public IEnumerable<Keep> GetKeepsByUser(string id)
    {
      return _repo.GetKeepsByUser(id);
    }

    public Keep Create(Keep newKeep)
    {
      int id = _repo.Create(newKeep);
      newKeep.Id = id;
      return newKeep;
    }

    public Keep Edit(Keep newKeep)
    {
      Keep keep = _repo.Get(newKeep.Id);
      if (keep == null) { throw new Exception("Invalid Id, Yo."); }
      keep.shares = newKeep.shares;
      keep.hasBeenKept = newKeep.hasBeenKept;
      keep.views = newKeep.views;
      _repo.Edit(keep);
      return keep;
    }

    public Keep AddKeep(int id, int keepId, string userId)
    {
      return _repo.Get(id);
      //   if (keep == null || keep.userId != userId) { throw new Exception("Invalid id, Yo."); }

    }


    public string Delete(int id, string userId)
    {
      Keep keep = _repo.Get(id);
      if (keep == null || keep.userId != userId) { throw new Exception("Invalid info, Yo."); }
      _repo.Delete(id);
      return "Success";
    }
  }
}