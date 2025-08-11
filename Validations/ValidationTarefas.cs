using TrilhaApiDesafio.Models;

namespace TrilhaApiDesafio.Validations;

public static class ValidationTarefas
{
    public static string? ValidateDate(Tarefa tarefa)
    {
        if (tarefa.Date == DateTime.MinValue)
        {
            return "A data da tarefa não pode ser vazia.";
        }

        return null; // sem erro
    }
}