from diagrams import Diagram
from diagrams.c4 import Person, SystemBoundary, Container, System, Relationship, Database

graph_attr = {
    "splines": "spline",
}

with Diagram("Container Diagram For Profio Logistics Application", direction="TB", graph_attr=graph_attr):
  customer = Person(
    name="Customer",
    description="A customer of Profio who can create orders and track their status",
  )

  staff = Person(
    name="Staff",
    description="A staff member of Profio who can manage the content of the web application",
    external=True,
  )

  with SystemBoundary("Profio Logistics Application"):
      webapp = Container(
        name="Web Application",
        technology="Blazor",
        description="The web application that allows customers to create orders and track their status",
      )

      cms = Container(
        name="Content Management System",
        technology="Next.js",
        description="The content management system that allows Profio to manage the content of the web application",
      )

      driverapp = Container(
        name="Driver Application",
        technology="Flutter",
        description="The mobile application that allows drivers to view and update the status of orders",
      )

      api = Container(
        name="API",
        technology="ASP.NET Core",
        description="The API that provides data to the web application and driver application",
      )

      db = Database(
        name="Database",
        technology="PostgreSQL",
        description="The database that stores data for the web application and driver application",
      )

      cache = Database(
        name="Cache",
        technology="Redis",
        description="The cache that stores data for the web application and driver application",
      )

  email = System(
    name="Email",
    description="The email system that sends order status updates to customers",
    external=True,
  )

  customer >> Relationship("Visits the web application using [HTTPS]") >> webapp
  staff >> Relationship("Manages system]") >> [cms, driverapp]
  [webapp, cms, driverapp] >> Relationship("Makes api call using [JSON/HTTPS]") >> api
  api >> Relationship("Queries data from") >> [db, cache]
  api >> Relationship("Sends email using [SMTP]") >> email

