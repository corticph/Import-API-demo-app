using System.Collections.Generic;

namespace ImportAPIClient.Repo
{

    public interface ICaseRepo
    {
        ICollection<Entity.Case> GetAllCases();

    }
}
