using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using TrilhaApiDesafio.Context;
using TrilhaApiDesafio.Models;

namespace TrilhaApiDesafio.Services;

public class TarefaService
{
    private readonly OrganizadorContext _contextRepository;

    public TarefaService(OrganizadorContext contextRepository)
    {
        this._contextRepository = contextRepository;
    }

    public List<Tarefa> FindAll()
    {
        return _contextRepository.Tarefas.ToList();
    }

    public Tarefa FindById(int id)
    {
        return _contextRepository.Tarefas.Find(id);
    }

    public List<Tarefa> FindBy(EnumFilterType filtro, object value)
    {
        var query = _contextRepository.Tarefas.AsQueryable();

        switch (filtro)
        {
            case EnumFilterType.Title when value is string titulo:
                query = query.Where(t => t.Titulo.ToLower().Contains(titulo.ToLower()));
                break;

            case EnumFilterType.Date when value is DateTime date:
                query = query.Where(t => t.Date.Date == date.Date);
                break;

            case EnumFilterType.Status when value is EnumStatusTarefa status:
                query = query.Where(t => t.Status == status);
                break;

            default:
                return new List<Tarefa>();
        }

        return query.ToList();
    }

    public void Add(Tarefa tarefa)
    {
        _contextRepository.Tarefas.Add(tarefa);
        _contextRepository.SaveChanges();
    }

    public void Update(Tarefa tarefa)
    {
        _contextRepository.Tarefas.Update(tarefa);
        _contextRepository.SaveChanges();
    }

    public void Delete(Tarefa tarefa)
    {
        _contextRepository.Tarefas.Remove(tarefa);
        _contextRepository.SaveChanges();
    }
}