using evalproject.models;
using evalproject.repo;

namespace evalproject.services
{
    public class logservice : IEvalser<Login>
    {
        private readonly evalrepo<Login> eval;

        public logservice( evalrepo<Login> service)
        {
            eval = service;
        }
        public bool login(Login l)
        {
            return(eval.login(l));
        }

        public bool update(Login l)
        {
            return(eval.update(l));
        }
    }
}
