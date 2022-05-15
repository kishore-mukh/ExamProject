namespace evalproject.models
{
    public interface Imodel<Login>
    {
        public bool login(Login l);
        public bool update(Login l);
    }
}
