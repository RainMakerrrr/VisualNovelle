using Naninovel;
using UnityEngine.SceneManagement;

namespace MatchGame.Commands
{
    [CommandAlias("startMatchGame")]
    public class StartMatchGameCommand : Command
    {
        private const string FindMatchGame = "FindMatch";

        public override UniTask ExecuteAsync(AsyncToken asyncToken = default)
        {
            SceneManager.LoadScene(FindMatchGame);
            
            return UniTask.CompletedTask;
        }
    }
}