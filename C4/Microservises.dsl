workspace {
  model {
    User = person "User" "Uses the application"
    Manager = person "Manager" "Manages the system"
    Courier = person "Courier" "Delivers the products"

    OnlineStore = softwareSystem "Online Store" {
        
        WebUI = container "WebUI" "NextJS application to serve managers and buyers"
        MobileUI = container  "MobileUI" "Mobile app to serve couriers and buyers"
        traffic = container "API Gateway" "API Gateway to route incoming traffic to the respective microservice" "" "Gateway" 
        service1 = group "Service 1" {
            OrderProcessing = container  "Order Processing" "Microservice to handle the processing of orders" "" "Service API" {
                tags "Service 1"
            }
            OrderDB = container  "Order Database" "Database to store Order data" "" "Database" {
                tags "Service 1"
            }
            OrderProcessing -> OrderDB "Interacts with"
        }
        service2 = group "Service 2" {
            InventoryManagement = container  "Inventory Management" "Microservice to manage the warehouse inventory" "" "Service API" {
                tags "Service 2"
            }
            InventoryDB = container  "Inventory Database" "Database to store Inventory data" "" "Database" {
                tags "Service 2"
            }
            InventoryManagement -> InventoryDB "Interacts with"
        }
        service3 = group "Service 3" {
            UserManagement = container  "User Management" "Microservice to handle User data and authentication" "" "Service API" {
                tags "Service 3"
            }
        }
   
        service4 = group "Service 4" {
            DeliveryManagement = container  "Delivery Management" "Microservice to handle the delivery of products" "" "Service API" {
                tags "Service 4"
            }
            DeliveryDB = container  "Delivery Database" "Database to store Delivery data" "" "Database" {
                tags "Service 4"
            }
            DeliveryManagement -> DeliveryDB "Interacts with" 
        }
        EventStream = container "CQRS/ES Event Stream" "Event stream to capture state changes from microservices" "" "Event Stream"

        ReportingDB = container "Reporting Database" "Database to store report data" "" "Database"

    
        WebUI -> traffic "Routes requests to"
        MobileUI -> traffic "Routes requests to"
        traffic -> OrderProcessing "Routes requests to"
        traffic -> InventoryManagement "Routes requests to"
        traffic -> UserManagement "Routes requests to"
        traffic -> DeliveryManagement "Routes requests to"


        OrderProcessing -> EventStream "Pushes state changes"
        InventoryManagement -> EventStream "Pushes state changes"
        UserManagement -> EventStream "Pushes state changes"
        DeliveryManagement -> EventStream "Pushes state changes"

        EventStream -> ReportingDB "Streams state changes"
    }

    ThirdPartyReportingApp = softwareSystem "Third-Party Reporting System" "External system for generating reports"
    CDP = softwareSystem "Customer Data Platform" "Handles user authentication and profile management"

    UserManagement -> CDP "Interacts with for user authentication"

    ReportingDB -> ThirdPartyReportingApp "Provides data for report generation"

    User -> WebUI "Uses"
    User -> MobileUI "Uses"
    Manager -> WebUI "Uses"
    Courier -> MobileUI "Uses"

  }

  views {
    themes "https://static.structurizr.com/themes/microsoft-azure-2021.01.26/theme.json" "https://static.structurizr.com/themes/kubernetes-v0.3/theme.json" "https://structurizr.com/share/38000/theme"
    
    styles {
        element "Service API" {
            shape hexagon
        }
        
        element "Service 1" {
            background #91F0AE
        }
        element "Service 2" {
            background #EDF08C
        }
        element "Service 3" {
            background #8CD0F0
        }
        element "Service 4" {
            background #F08CA4
        }
    }

    systemContext OnlineStore {
      include *
      autoLayout
    }

    container OnlineStore {
      include *
      autoLayout
    }
  }
}