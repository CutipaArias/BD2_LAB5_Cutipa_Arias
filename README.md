# SESION DE LABORATORIO N° 05: Construyendo una aplicacion con una base de datos de Series de Tiempo

### Nombre

## OBJETIVOS
  * Comprender como se construyen aplicaciones con bases de datos de Series de Tiempo.

## REQUERIMIENTOS
  * Conocimientos: 
    - Conocimientos básicos de gestión de base de datos de series de tiempo
    - Conocimientos básicos de shell (linea de comandos).
  * Hardware:
    - Virtualization activada en el BIOS..
    - CPU SLAT-capable feature.
    - Al menos 4GB de RAM.
  * Software:
    - Windows 10 64bit: Pro, Enterprise o Education (1607 Anniversary Update, Build 14393 o Superior)
    - Docker Desktop 
    - Powershell versión 7.x
    - Gestores de paquetes: Winget, Choco o Scoop
    - Node Version Manager 1.11 (winget install nvm)

## CONSIDERACIONES INICIALES
  * Clonar el repositorio mediante git para tener los recursos necesarios.
  
## DESARROLLO

1. Iniciar la aplicación Powershell o Windows Terminal en modo administrador.
2. Copiar la aplicación desarrollada en el Laboratorio 02 (https://github.com/UPT-FAING-EPIS/lab_bd_relacional_02)
3. En el Terminal, ejecutar el siguiente comando si existe la imagen del laboratorio anterior.
```Bash
docker rmi -f serviciocliente-api
```  
4. En el Terminal, ejecutar el siguiente comando para instalar las dependencias necesarias para el funcionamiento del proyecto.
```Bash
dotnet add .\ClienteAPI\ package prometheus-net.AspNetCore --version 7.0.0
dotnet add .\ClienteAPI\ package prometheus-net.AspNetCore.HealthChecks --version 7.0.0
```
5. Iniciar Visual Studio Code apuntando a la carpeta recien creada, o desde el terminal ejecutar el comando `code .` para iniciarlo. 
6. En Visual Studio Code, modificar el archivo program.cs con el siguiente contenido:
```C#
using ClienteAPI.Data;
using Microsoft.EntityFrameworkCore;
using Prometheus;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<BdClientesContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("ClienteDB")));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseMetricServer();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseHttpMetrics();
app.MapControllers();

app.Run();
```  
7. En Visual Studio Code, dentro la raiz del proyecto crear la siguiente estructura de archivos, con el siguiente contenido

![image](https://github.com/UPT-FAING-EPIS/lab_bd_no_relacional_05/assets/10199939/1c447d0f-afb7-444f-bdb7-b0dd69439547)

> APIDashboard.json
```JSON
{
    "annotations": {
      "list": [
        {
          "builtIn": 1,
          "datasource": "-- Grafana --",
          "enable": true,
          "hide": true,
          "iconColor": "rgba(0, 211, 255, 1)",
          "name": "Annotations & Alerts",
          "target": {
            "limit": 100,
            "matchAny": false,
            "tags": [],
            "type": "dashboard"
          },
          "type": "dashboard"
        }
      ]
    },
    "description": "",
    "editable": true,
    "gnetId": null,
    "graphTooltip": 0,
    "id": 2,
    "links": [],
    "liveNow": false,
    "panels": [
      {
        "datasource": "Prometheus",
        "fieldConfig": {
          "defaults": {
            "color": {
              "mode": "palette-classic"
            },
            "custom": {
              "axisLabel": "",
              "axisPlacement": "auto",
              "barAlignment": 0,
              "drawStyle": "line",
              "fillOpacity": 0,
              "gradientMode": "none",
              "hideFrom": {
                "legend": false,
                "tooltip": false,
                "viz": false
              },
              "lineInterpolation": "linear",
              "lineWidth": 1,
              "pointSize": 5,
              "scaleDistribution": {
                "type": "linear"
              },
              "showPoints": "auto",
              "spanNulls": false,
              "stacking": {
                "group": "A",
                "mode": "none"
              },
              "thresholdsStyle": {
                "mode": "off"
              }
            },
            "mappings": [],
            "thresholds": {
              "mode": "absolute",
              "steps": [
                {
                  "color": "green",
                  "value": null
                },
                {
                  "color": "red",
                  "value": 80
                }
              ]
            }
          },
          "overrides": []
        },
        "gridPos": {
          "h": 8,
          "w": 12,
          "x": 0,
          "y": 0
        },
        "id": 4,
        "options": {
          "legend": {
            "calcs": [],
            "displayMode": "list",
            "placement": "bottom"
          },
          "tooltip": {
            "mode": "single"
          }
        },
        "targets": [
          {
            "exemplar": true,
            "expr": "rate(http_request_duration_seconds_count{}[$__interval])",
            "format": "time_series",
            "interval": "",
            "legendFormat": "",
            "refId": "A"
          }
        ],
        "title": "HTTP Request Rate",
        "type": "timeseries"
      },
      {
        "datasource": "Prometheus",
        "fieldConfig": {
          "defaults": {
            "color": {
              "mode": "palette-classic"
            },
            "custom": {
              "axisLabel": "",
              "axisPlacement": "auto",
              "barAlignment": 0,
              "drawStyle": "line",
              "fillOpacity": 0,
              "gradientMode": "none",
              "hideFrom": {
                "legend": false,
                "tooltip": false,
                "viz": false
              },
              "lineInterpolation": "linear",
              "lineWidth": 1,
              "pointSize": 5,
              "scaleDistribution": {
                "type": "linear"
              },
              "showPoints": "auto",
              "spanNulls": false,
              "stacking": {
                "group": "A",
                "mode": "none"
              },
              "thresholdsStyle": {
                "mode": "off"
              }
            },
            "mappings": [],
            "thresholds": {
              "mode": "absolute",
              "steps": [
                {
                  "color": "green",
                  "value": null
                },
                {
                  "color": "red",
                  "value": 80
                }
              ]
            }
          },
          "overrides": []
        },
        "gridPos": {
          "h": 8,
          "w": 12,
          "x": 12,
          "y": 0
        },
        "id": 6,
        "options": {
          "legend": {
            "calcs": [],
            "displayMode": "list",
            "placement": "bottom"
          },
          "tooltip": {
            "mode": "single"
          }
        },
        "targets": [
          {
            "exemplar": true,
            "expr": "process_num_threads{}",
            "interval": "",
            "legendFormat": "",
            "refId": "A"
          }
        ],
        "title": "No Of Threads",
        "type": "timeseries"
      },
      {
        "datasource": "Prometheus",
        "fieldConfig": {
          "defaults": {
            "color": {
              "mode": "palette-classic"
            },
            "custom": {
              "axisLabel": "",
              "axisPlacement": "auto",
              "barAlignment": 0,
              "drawStyle": "line",
              "fillOpacity": 0,
              "gradientMode": "none",
              "hideFrom": {
                "legend": false,
                "tooltip": false,
                "viz": false
              },
              "lineInterpolation": "linear",
              "lineWidth": 1,
              "pointSize": 5,
              "scaleDistribution": {
                "type": "linear"
              },
              "showPoints": "auto",
              "spanNulls": false,
              "stacking": {
                "group": "A",
                "mode": "none"
              },
              "thresholdsStyle": {
                "mode": "off"
              }
            },
            "mappings": [],
            "thresholds": {
              "mode": "absolute",
              "steps": [
                {
                  "color": "green",
                  "value": null
                },
                {
                  "color": "red",
                  "value": 80
                }
              ]
            }
          },
          "overrides": []
        },
        "gridPos": {
          "h": 9,
          "w": 12,
          "x": 0,
          "y": 8
        },
        "id": 2,
        "options": {
          "legend": {
            "calcs": [],
            "displayMode": "list",
            "placement": "bottom"
          },
          "tooltip": {
            "mode": "single"
          }
        },
        "targets": [
          {
            "exemplar": true,
            "expr": "process_virtual_memory_bytes{}",
            "interval": "",
            "legendFormat": "",
            "refId": "A"
          }
        ],
        "title": "Process Virtual Memory Bytes",
        "type": "timeseries"
      }
    ],
    "refresh": "5s",
    "schemaVersion": 31,
    "style": "dark",
    "tags": [],
    "templating": {
      "list": []
    },
    "time": {
      "from": "now-5m",
      "to": "now"
    },
    "timepicker": {},
    "timezone": "",
    "title": "API Metrics Dashboard",
    "uid": "TVo22zwnz",
    "version": 2
  }
```  
> dashboard.yml
```YAML
apiVersion: 1
providers:
  - name: dashboards
    type: file
    updateIntervalSeconds: 3
    options:
      path: /etc/grafana/provisioning/dashboards
      foldersFromFilesStructure: true
```  
> prometheus_ds.yml
```YAML
datasources:
- name: Prometheus
  access: proxy
  type: prometheus
  url: http://prometheus:9090
  isDefault: true
```  
> prometheus.yml
```YAML
global:
  scrape_interval: 3s

scrape_configs:
  - job_name: 'apiCliente'
    scheme: http
    metrics_path: /metrics
    static_configs:
      - targets: 
        - host.docker.internal:5000
```  
8. En Visual Studio Code, en la raiz del proyecto modificar el archivo docker-compose.yml, con el siguiente contenido:
> docker-compose.yml
```Yaml
version: "3.9"
services:

  bd:
    image: "mcr.microsoft.com/mssql/server"
    container_name: bd_clientes
    ports:
      - "1433:1433" 
    environment:
      - ACCEPT_EULA=y
      - SA_PASSWORD=Upt.2022
    volumes:
      - ./db:/tmp

  api:
    build: ClienteAPI/. # build the Docker image 
    container_name: api_clientes
    ports:
      - "5000:80"
    environment:
      - ConnectionStrings__ClienteDB=Server=bd_clientes;Database=BD_CLIENTES;User Id=sa;Password=Upt.2022;TrustServerCertificate=true
    depends_on:
      - bd

  prometheus:
    container_name: prometheus
    image: prom/prometheus
    restart: always
    ports:
      - 9090:9090
    volumes:
      - ./metrics/prometheus/prometheus.yml:/etc/prometheus/prometheus.yml:ro
    depends_on:
      - api

  grafana:
    container_name: grafana
    image: grafana/grafana
    ports:
      - "3000:3000"
    volumes:
        - ./metrics/grafana_ds:/etc/grafana/provisioning/datasources
        - ./metrics/grafana_db:/etc/grafana/provisioning/dashboards
    depends_on:
      - prometheus

```  
9. En el terminal, ejecutar el siguiente comando para construir el contenedor correspondiente de la aplicación.
```Bash
docker-compose up -d
```
10. Finalmente en el terminal, esperar 30 segundos y ejecutar el siguiente comando para realizar la migración de la base de datos necesaria para que la aplicación funcione.
```Bash
docker exec -it bd_clientes /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P Upt.2022 -i /tmp/clientes.sql
```
11. En un navegador de internet, colocar la dirección http://localhost:5000/swagger para verificar que la apicación se ejecuta correctamente.
![image](https://github.com/UPT-FAING-EPIS/lab_bd_no_relacional_05/assets/10199939/d992e242-ecdb-4afa-80ab-e3ed3be331b6)

12. En el mismo terminal o en dos en paralelo utilice los siguientes comandos para generar trafico hacia el servicio o api.
```Bash
for($i=1;$i -le 100;$i++) { curl 'http://localhost:5000/api/TiposDocumentos'; Start-Sleep -s 1; }
for($i=1;$i -le 100;$i++) { curl 'http://localhost:5000/api/TiposDocumentos/1'; Start-Sleep -s 1; }
```  
15. En un navegador de intenet, colocar la direccion http://localhost:3000/, usuario: admin, password: admin (la primera vez pide cambio de contraseña colocar igualmente en los dos casilleros admin) para verificar que las metricas estan siendo generados en la base de datos Prometheus.
![image](https://github.com/UPT-FAING-EPIS/lab_bd_no_relacional_05/assets/10199939/f231dd24-e20b-4f51-935d-779219f79db7)

---
## Actividades Encargadas
1. Implementar una metrica nueva que evalue HealthChecks para la base de datos y crear el grafico en Grafana asociado.
2. Genere una automatizacion buildimage.yml para construir la imagen y publicarla como paquete en su repositorio
3. Genere una automatizacion delivery.yml, que se active de manera manual (WORKFLOW_DISTPACH), instanciando las imagenes y pueda hacer una petición GET sobre Prometheus mostrando la información de una metrica.
