from diagrams import Diagram
from diagrams.c4 import SystemBoundary, Container, Relationship, Database

graph_attr = {
    "splines": "spline",
}

tech = "ASP.NET Core Rest API"

with (Diagram("Component Diagram For Profio Logistics Application", direction="TB", graph_attr=graph_attr)):
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

  with SystemBoundary("Profio Logistics Application"):
      auth = Container(
        name="Access Control",
        technology=tech,
        description="An access control service that provides authentication and authorization for the web application and driver application",
      )

      orders_catalog = Container(
        name="Orders Catalog",
        technology=tech,
        description="The orders catalog service that provides order data to the web application and driver application",
      )

      driver_catalog = Container(
        name="Driver Catalog",
        technology=tech,
        description="The driver catalog service that provides driver data to the web application and driver application",
      )

      hub_management = Container(
        name="Hub Management",
        technology=tech,
        description="The hub management service that provides hub data to the web application and driver application",
      )

  db = Database(
    name="Database",
    technology="PostgreSQL/Redis",
    description="The database that stores data for the web application and driver application",
  )

  [cms, driverapp] >> Relationship("Provides data to") >> auth
  webapp >> Relationship("Get own orders information from") >> orders_catalog
  auth >> Relationship("Manages data in") >> [orders_catalog, hub_management, driver_catalog]
  driver_catalog >> Relationship("Provides data to") >> hub_management
  [orders_catalog, hub_management, driver_catalog] >> Relationship("Stores/Retrieves data from") >> db
