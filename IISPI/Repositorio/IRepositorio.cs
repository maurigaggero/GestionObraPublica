namespace IISPI.Repositorio
{
    public interface IRepositorio<E>
    {
        Task<bool> Baja(int id);
        Task<bool> Delete(int id);
        Task<List<E>> GetActivos();
        Task<E> GetById(int id);
        Task<List<E>> GetFull();
        Task<int> Insert(E entity);
        Task<bool> Update(E entrada, int id);
    }
}