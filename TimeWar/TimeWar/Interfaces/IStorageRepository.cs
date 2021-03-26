using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeWar.Interfaces
{
    public interface IStorageRepository
    {
        void SaveGame();

        void LoadGame();

        void StoreHighScore();

        void LoadHighScore();
    }
}
