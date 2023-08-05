using Ardalis.Specification;
using Domain.Entities;

namespace Application.Specifications
{
    public class PagedClientSpecification:Specification<Client>
    {
        public PagedClientSpecification(int pageSize,int pageNumber,string name,string lastName)
        {
            Query.Skip((pageNumber-1)*pageSize).Take(pageSize); // Skip and take row register

            if(!string.IsNullOrEmpty(lastName))
                Query.Search(x=> x.LastName,"%"+lastName+"%"); //query type Like

            if (!string.IsNullOrEmpty(name))
                Query.Search(x => x.FirstName, "%" + name + "%");

        }
    }
}
