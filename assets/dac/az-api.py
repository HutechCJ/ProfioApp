from diagrams import Cluster, Diagram, Edge
from diagrams.azure.web import AppServiceEnvironments
from diagrams.onprem.database import PostgreSQL
from diagrams.onprem.inmemory import Redis
from diagrams.custom import Custom
from diagrams.onprem.monitoring import Prometheus
from diagrams.onprem.monitoring import Grafana
from diagrams.onprem.tracing import Jaeger
from diagrams.elastic.elasticsearch import Elasticsearch
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
    api = AppServiceEnvironments("API")
    db = PostgreSQL("Database")
    cache = Redis("Cache")
    api >> db
    api >> cache

  lb >> api
  api >> otelcollector
  Mobile("Driver App") >> broker >> api
  api >> hc

  with Cluster("External Service"):
    Prometheus = Prometheus("Prometheus")
    Grafana = Grafana("Grafana")
    Jaeger = Jaeger("Jaeger")
    ELK = Elasticsearch("Elasticsearch")
    Zipkin = Custom("Zipkin", "./resources/zipkin.png")

  otelcollector >> Prometheus
  otelcollector >> Grafana
  otelcollector >> Jaeger
  otelcollector >> ELK
  otelcollector >> Zipkin
