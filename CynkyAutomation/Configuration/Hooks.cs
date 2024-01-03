using CynkyHook;
using CynkyUtilities.ZephyrScale;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;

[assembly: Parallelize(Workers = 0, Scope = ExecutionScope.ClassLevel)]
namespace CynkyAutomation.Configuration
{
    [Binding]
    class Hooks
    {
        Config _Config;
        ZephyrClient _ZephyrClient;
        FeatureContext _FeatureContext;
        ScenarioContext _ScenarioContext;

        Hooks(ScenarioContext scenarioContext)
        {
            _FeatureContext = scenarioContext.ScenarioContainer.Resolve<FeatureContext>();
            _ScenarioContext = scenarioContext.ScenarioContainer.Resolve<ScenarioContext>();
            _Config = scenarioContext.ScenarioContainer.Resolve<Config>();
            _ZephyrClient = new ZephyrClient(ConfigManager.ZephyrBearerToken, ConfigManager.ZephyrServiceUrl, ConfigManager.ZephyrProjectKey);
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
        void AfterScenario()
        {
            _Config.AfterScenario(_ScenarioContext);
            if (ConfigManager.PublishToZephyr.ToLower() == "true")
                _ZephyrClient.UpdateResultsToZephyr(_FeatureContext, _ScenarioContext);

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
