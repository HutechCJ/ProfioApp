from diagrams import Diagram
from diagrams.c4 import Person, SystemBoundary, Container, System, Relationship

graph_attr = {
    "splines": "spline",
}

with Diagram("Context Diagram For Profio Logistics Application",
             direction="TB",
             graph_attr=graph_attr):
    customer = Person(
        name="Customer",
        description=
        "A customer of Profio who can create orders and track their status",
    )

    staff = Person(
        name="Staff",
        description=
        "A staff member of Profio who can manage the content of the web application",
        external=True,
    )

    with SystemBoundary("Profio Logistics Application"):
        system = Container(
            name="Profio Logistics System",
            description=
            "The system that allows customers to create orders and track their status",
        )

        email = System(
            name="Email",
            description=
            "The email system that sends order status updates to customers",
            external=True,
        )

    customer >> Relationship(
        "Creates orders and tracks their status") >> system
    system >> Relationship("Sends order status updates by") >> email
    staff >> Relationship("Updates the status of orders") >> system
