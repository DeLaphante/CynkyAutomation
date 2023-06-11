using BoDi;
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

        public BookingStepDefinitions(IObjectContainer objectContainer)
        {
            _Request = objectContainer.Resolve<Request>();
            _Response = objectContainer.Resolve<Response>();
            _Headers = objectContainer.Resolve<Headers>();
            _ScenarioContext = objectContainer.Resolve<ScenarioContext>();
        }

        [Given(@"an auth token is received")]
        public void GivenAnAuthTokenIsReceived()
        {
            var json = new AuthRequest { password = "password123", username = "admin" };

            _Request.SerializeJson(json, _ScenarioContext);


            _Request.SendRequestToEndpoint(Method.POST, BookingEndpointBuilder.GetAuthEndPoint());
            _Response.GetResponseBody<AuthResponse>().token.Should().NotBeNullOrEmpty();
            _ScenarioContext.Set<string>(_Response.GetResponseBody<AuthResponse>().token, "token");
        }

        [When(@"a get request is made to the booking endpoint")]
        public void WhenAGetRequestIsMadeToTheBookingEndpoint()
        {
            _Request.SendRequestToEndpoint(Method.GET, BookingEndpointBuilder.GetBookingEndPoint());
        }

        [Then(@"the response should contain a list of booking ids")]
        public void ThenTheResponseShouldContainAListOfBookingIds()
        {
            _Response.GetStatusCode().Should().Be(HttpStatusCode.OK);
            _Response.GetResponseBody<List<GetBookingIdsResponse>>().Should().NotBeNullOrEmpty();
            _ScenarioContext.Set<int>(_Response.GetResponseBody<List<GetBookingIdsResponse>>().Count, "numberOfIds");
        }

        [When(@"a post request is made to the create booking endpoint")]
        public void WhenAPostRequestIsMadeToTheCreateBookingEndpoint()
        {
            _Headers.AddHeader("Accept", "application/json");

            var json = BookingJsonBuilder.GetCreateBookingData();

            _Request.SerializeJson(json, _ScenarioContext);


            _Request.SendRequestToEndpoint(Method.POST, BookingEndpointBuilder.GetBookingEndPoint());

            var createBookingResponse = _Response.GetResponseBody<CreateBookingResponse>();

            _ScenarioContext.Set<int>(createBookingResponse.bookingid, "bookingid");
        }

        [Then(@"the response should contain a booking id")]
        public void ThenTheResponseShouldContainABookingId()
        {
            _Response.GetStatusCode().Should().Be(HttpStatusCode.OK);
            _ScenarioContext.Get<int>("bookingid").Should().NotBe(0);
        }
    }
}
