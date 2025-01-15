using CynkyAutomation.EndpointBuilder;
using CynkyAutomation.JsonBuilder.BookingService;
using CynkyAutomation.Models.API.Request;
using CynkyAutomation.Models.API.Response;
using CynkyHttp;
using FluentAssertions;
using System.Collections.Generic;
using System.Net;
using Reqnroll;

namespace CynkyAutomation.StepDefinitions.API
{
    [Binding]
    public class BookingStepDefinitions
    {
        CynkyClient _CynkyClient;
        ScenarioContext _ScenarioContext;

        public BookingStepDefinitions(ScenarioContext scenarioContext)
        {
            _CynkyClient = scenarioContext.ScenarioContainer.Resolve<CynkyClient>();
            _ScenarioContext = scenarioContext.ScenarioContainer.Resolve<ScenarioContext>();
        }

        [StepDefinition(@"an auth token is received")]
        public void GivenAnAuthTokenIsReceived()
        {
            var json = new AuthRequest { password = "password123", username = "admin" };

            _CynkyClient.SendRequest(Method.POST, BookingEndpointBuilder.GetAuthEndPoint(), json);
            _CynkyClient.GetResponseBody<AuthResponse>().token.Should().NotBeNullOrEmpty();
            _ScenarioContext.Set<string>(_CynkyClient.GetResponseBody<AuthResponse>().token, "token");
        }

        [StepDefinition(@"a get request is made to the booking endpoint")]
        public void WhenAGetRequestIsMadeToTheBookingEndpoint()
        {
            _CynkyClient.SendRequest(Method.GET, BookingEndpointBuilder.GetBookingEndPoint());
        }

        [StepDefinition(@"the response should contain a list of booking ids")]
        public void ThenTheResponseShouldContainAListOfBookingIds()
        {
            _CynkyClient.GetStatusCode().Should().Be(HttpStatusCode.OK);
            _CynkyClient.GetResponseBody<List<GetBookingIdsResponse>>().Should().NotBeNullOrEmpty();
            _ScenarioContext.Set<int>(_CynkyClient.GetResponseBody<List<GetBookingIdsResponse>>().Count, "numberOfIds");
        }

        [StepDefinition(@"a post request is made to the create booking endpoint")]
        public void WhenAPostRequestIsMadeToTheCreateBookingEndpoint()
        {
            _CynkyClient.AddHeader("Accept", "application/json");

            var json = BookingJsonBuilder.GetCreateBookingData();

            _CynkyClient.SendRequest(Method.POST, BookingEndpointBuilder.GetBookingEndPoint(), json);

            var createBookingResponse = _CynkyClient.GetResponseBody<CreateBookingResponse>();

            _ScenarioContext.Set<int>(createBookingResponse.bookingid, "bookingid");
        }

        [StepDefinition(@"the response should contain a booking id")]
        public void ThenTheResponseShouldContainABookingId()
        {
            _ScenarioContext.Set(_CynkyClient.GetResponseHeaders(), "responseHeaders");
            _CynkyClient.GetStatusCode().Should().Be(HttpStatusCode.OK);
            _CynkyClient.GetResponseHeaders().Should().NotBeNullOrEmpty();
            _ScenarioContext.Get<int>("bookingid").Should().NotBe(0);
        }
    }
}
