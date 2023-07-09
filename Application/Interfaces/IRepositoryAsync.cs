using Ardalis.Specification;

namespace Application.Interfaces
{
    public interface IRepositoryAsync<T>:IRepositoryBase<T> where T: class // methods for write only
    {
    }

    public interface IReadRepositoryAsync<T> :IReadRepositoryBase<T> where T:class { } //methods for read only
}
