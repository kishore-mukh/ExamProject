using evalproject.models;

namespace evalproject.repo
{
    public class evalrepoclass : evalrepo<Login>
    {
        private readonly Imodel<Login> model;
        public evalrepoclass(Imodel<Login> p)
        {
            model = p;
        }
        public bool login(Login l)
        {
            return model.login(l);
        }

        public bool update(Login l)
        {
            throw new System.NotImplementedException();
        }
    }
}
