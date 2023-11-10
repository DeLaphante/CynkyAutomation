using CynkyAutomation.EndpointBuilder;
using CynkyAutomation.JsonBuilder.BookingService;
using CynkyAutomation.Models.API.Request;
using CynkyAutomation.Models.API.Response;
using CynkyHttp;
using FluentAssertions;
using System.Collections.Generic;
using System.Net;
using TechTalk.SpecFlow;

namespace CynkyAutomation.StepDefinitions.API
{
    [Binding]
    public class BookingStepDefinitions
    {
        Request _Request;
        Response _Response;
        Headers _Headers;
        ScenarioContext _ScenarioContext;

        public BookingStepDefinitions(ScenarioContext scenarioContext)
        {
            _Request = scenarioContext.ScenarioContainer.Resolve<Request>();
            _Response = scenarioContext.ScenarioContainer.Resolve<Response>();
            _Headers = scenarioContext.ScenarioContainer.Resolve<Headers>();
            _ScenarioContext = scenarioContext.ScenarioContainer.Resolve<ScenarioContext>();
        }

        [StepDefinition(@"an auth token is received")]
        public void GivenAnAuthTokenIsReceived()
        {
            var json = new AuthRequest { password = "password123", username = "admin" };

            _Request.SendRequest(Method.POST, BookingEndpointBuilder.GetAuthEndPoint(), json);
            _Response.GetResponseBody<AuthResponse>().token.Should().NotBeNullOrEmpty();
            _ScenarioContext.Set<string>(_Response.GetResponseBody<AuthResponse>().token, "token");
        }

        [StepDefinition(@"a get request is made to the booking endpoint")]
        public void WhenAGetRequestIsMadeToTheBookingEndpoint()
        {
            _Request.SendRequest(Method.GET, BookingEndpointBuilder.GetBookingEndPoint());
        }

        [StepDefinition(@"the response should contain a list of booking ids")]
        public void ThenTheResponseShouldContainAListOfBookingIds()
        {
            _Response.GetStatusCode().Should().Be(HttpStatusCode.OK);
            _Response.GetResponseBody<List<GetBookingIdsResponse>>().Should().NotBeNullOrEmpty();
            _ScenarioContext.Set<int>(_Response.GetResponseBody<List<GetBookingIdsResponse>>().Count, "numberOfIds");
        }

        [StepDefinition(@"a post request is made to the create booking endpoint")]
        public void WhenAPostRequestIsMadeToTheCreateBookingEndpoint()
        {
            _Headers.AddHeader("Accept", "application/json");

            var json = BookingJsonBuilder.GetCreateBookingData();

            _Request.SendRequest(Method.POST, BookingEndpointBuilder.GetBookingEndPoint(), json);

            var createBookingResponse = _Response.GetResponseBody<CreateBookingResponse>();

            _ScenarioContext.Set<int>(createBookingResponse.bookingid, "bookingid");
        }

        [StepDefinition(@"the response should contain a booking id")]
        public void ThenTheResponseShouldContainABookingId()
        {
            _ScenarioContext.Set(_Response.GetResponseHeaders(), "responseHeaders");
            _Response.GetStatusCode().Should().Be(HttpStatusCode.OK);
            _Response.GetResponseHeaders().Should().NotBeNullOrEmpty();
            _ScenarioContext.Get<int>("bookingid").Should().NotBe(0);
        }
    }
}
