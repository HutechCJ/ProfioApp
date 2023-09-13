from diagrams import Cluster, Diagram
from diagrams.azure.web import AppServiceEnvironments
from diagrams.onprem.database import PostgreSQL
from diagrams.onprem.inmemory import Redis
from diagrams.custom import Custom
from diagrams.onprem.monitoring import Prometheus
from diagrams.onprem.monitoring import Grafana
from diagrams.onprem.tracing import Jaeger
from diagrams.onprem.network import Yarp
from diagrams.onprem.client import Client
from diagrams.generic.device import Mobile

with Diagram("Profio API", show=False, direction="LR"):
    lb = Yarp("Load Balancer")
    otelcollector = Custom("OpenTelemetry Collector", "./resources/otel.png")
    broker = Custom("MQTT Broker", "./resources/emqtt.png")
    hc = Custom("Health Check", "./resources/health-check.png")

    Client("Client") >> lb

    with Cluster("Profio API"):
        api_1 = AppServiceEnvironments("API Sever 1")
        api_2 = AppServiceEnvironments("API Sever 2")
        db = PostgreSQL("Database")
        cache = Redis("Cache")
        api_1 >> [db, cache]
        api_2 >> [db, cache]

    lb >> [api_1, api_2]
    [api_1, api_2] >> otelcollector
    Mobile("Driver App") >> broker
    broker >> [api_1, api_2]
    api_1 >> hc
    api_2 >> hc

    with Cluster("External Service"):
        Prometheus = Prometheus("Prometheus")
        Grafana = Grafana("Grafana")
        Jaeger = Jaeger("Jaeger")
        seq = Custom("Seq", "./resources/seq.png")

    otelcollector >> Prometheus
    otelcollector >> Grafana
    otelcollector >> Jaeger
    otelcollector >> seq
