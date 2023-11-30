using GOP.BD.Data;
using GOP.BD.Data.Entity;

namespace GOP.Repositorio
{
    public interface IRepositorio<E>
    {
        BDContext Context { get; }
        Task<bool> Baja(int id, string usuarioId);
        Task<List<E>> GetActivos();
        Task<E> GetById(int id);
        Task<List<E>> GetFull();
        Task<int> Insert(E entity);
        Task<bool> Update(E entrada, int id);
    }
}