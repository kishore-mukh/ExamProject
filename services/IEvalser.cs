namespace evalproject.services
{
    public interface IEvalser<Login>
    { 
    
        public bool login(Login l);
        public bool update (Login l);
    }
}
