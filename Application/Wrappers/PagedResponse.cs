namespace Application.Wrappers
{
    public  class PagedResponse<T>:Response<T>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public PagedResponse(T data, int pageNumber,int pageSize)
        {
            this.Data = data;
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
            this.Message = null;
            this.Succeeded = true;
            this.Errors = null;
        }
    }
}
