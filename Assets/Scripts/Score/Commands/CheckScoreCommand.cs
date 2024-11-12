using Naninovel;
using Naninovel.Commands;

namespace Score.Commands
{
    [CommandAlias("checkScore")]
    public class CheckScoreCommand : Command
    {
        [ParameterAlias("value"), RequiredParameter]
        public IntegerParameter RequiredScore;

        [ParameterAlias("then")] public NamedStringParameter ThenLabel;

        [ParameterAlias("else")] public NamedStringParameter ElseLabel;

        public override async UniTask ExecuteAsync(AsyncToken asyncToken = default)
        {
            var scoreService = Engine.GetService<ScoreService>();
            if (scoreService == null)
            {
                return;
            }

            bool hasEnoughScore = scoreService.IsScoreGreaterThan(RequiredScore);

            if (hasEnoughScore && ThenLabel.HasValue)
            {
                var gotoCommand = new Goto {Path = ThenLabel};
                await gotoCommand.ExecuteAsync(asyncToken);
            }
            else if (!hasEnoughScore && ElseLabel.HasValue)
            {
                var gotoCommand = new Goto {Path = ElseLabel};
                await gotoCommand.ExecuteAsync(asyncToken);
            }
        }
    }
}