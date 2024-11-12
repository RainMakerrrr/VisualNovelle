using Naninovel;

namespace Score.Commands
{
    [CommandAlias("addScore")]
    public class AddScoreCommand : Command
    {
        [ParameterAlias("value"), RequiredParameter]
        public IntegerParameter ScoreValue;

        public override UniTask ExecuteAsync(AsyncToken asyncToken = default)
        {
            var scoreService = Engine.GetService<ScoreService>();
            scoreService?.AddScore(ScoreValue);
            return UniTask.CompletedTask;
        }
    }
}