
namespace DataTelephone
{
    public abstract class Repository
    {
        protected StoreContext context;
        public Repository(StoreContext context)
        {
            this.context = context;
        }
    }
}
