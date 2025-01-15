using CynkyHook;
using CynkyUtilities.ZephyrScale;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reqnroll;

[assembly: Parallelize(Workers = 0, Scope = ExecutionScope.ClassLevel)]
namespace CynkyAutomation.Configuration
{
    [Binding]
    class Hooks
    {
        Config _Config;
        FeatureContext _FeatureContext;
        ScenarioContext _ScenarioContext;

        Hooks(ScenarioContext scenarioContext)
        {
            _FeatureContext = scenarioContext.ScenarioContainer.Resolve<FeatureContext>();
            _ScenarioContext = scenarioContext.ScenarioContainer.Resolve<ScenarioContext>();
            _Config = scenarioContext.ScenarioContainer.Resolve<Config>();
        }

        [BeforeTestRun]
        static void InitializeReport()
        {
            Config.InitializeReport();
        }

        [BeforeScenario]
        void Launch()
        {
            _Config.Launch(_FeatureContext, _ScenarioContext, ConfigManager.RS_User, ConfigManager.RS_Key);
        }

        [BeforeFeature]
        static void BeforeFeature(FeatureContext featureContext)
        {
            Config.BeforeFeature(featureContext);
        }

        [AfterScenario]
        void AfterScenario(ScenarioContext scenarioContext)
        {
            _Config.AfterScenario(_ScenarioContext);
            if (ConfigManager.PublishToZephyr.ToLower() == "true")
                new ZephyrClient(ConfigManager.ZephyrBearerToken, ConfigManager.ZephyrServiceUrl, ConfigManager.ZephyrProjectKey, scenarioContext).UpdateResultsToZephyr(_FeatureContext, _ScenarioContext);

        }

        [AfterStep]
        void Steps()
        {
            _Config.Steps(_FeatureContext, _ScenarioContext);
        }

        [AfterTestRun]
        static void TearDownReport()
        {
            Config.TearDownReport();
        }
    }
}
