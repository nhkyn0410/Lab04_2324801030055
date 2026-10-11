# Build notes

Build command: `dotnet build DotNetMicroservicesPoc.sln` (.NET SDK 9.0.304, all projects target net8.0).

Final result: 0 errors, 11 warnings (all in instructor code, see section 4).

## 1. MediatR registration differs from the lab guide

- Guide: `services.AddMediatR()` in `Startup.ConfigureServices`.
- Problem: MediatR 12 has no parameterless `AddMediatR()`; the registration needs a configuration that names the assemblies to scan.
- Fix: MediatR pinned to 12.3.0 and registered in every service with
  `services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Startup>());`

## 2. BlazorWasmClient: 57 errors (39 × CS0246, 18 × CS0234)

- When: first build after extracting the instructor folders and adding `AgentPortalApiGateway` and `BlazorWasmClient` to the solution. All 57 errors were in `BlazorWasmClient`; `AgentPortalApiGateway` built without errors.
- Cause: `BlazorWasmClient` uses commands, queries and DTOs from `DashboardService.Api`, `PolicySearchService.Api`, `PolicyService.Api` and `ProductService.Api`. After Task 1 these projects were empty, and `DashboardService.Api` did not exist (warning MSB9008: the referenced project does not exist).
- Fix, without changing instructor code:
  - added `DashboardService`, `DashboardService.Api`, `DashboardService.Test`, with the same structure as the other services;
  - added the contract types the client uses to the four `.Api` projects (table below);
  - added MediatR 12.3.0 to these `.Api` projects, because commands and queries implement `IRequest<TResult>`.
- Result: 0 errors.

| Project | Namespace | Types |
|---|---|---|
| `DashboardService.Api` | `Queries` | `GetTotalSalesQuery`, `GetTotalSalesResult`, `GetAgentsSalesQuery`, `GetAgentsSalesResult`, `GetSalesTrendsQuery`, `GetSalesTrendsResult` |
| `DashboardService.Api` | `Queries.Dtos` | `SalesDto`, `PeriodSaleDto`, `TimeUnit` |
| `PolicySearchService.Api` | `Queries` | `FindPolicyQuery`, `FindPolicyResult` |
| `PolicySearchService.Api` | `Queries.Dtos` | `PolicyDto` |
| `PolicyService.Api` | `Commands` | `CreateOfferCommand`, `CreateOfferResult`, `CreatePolicyCommand`, `CreatePolicyResult` |
| `PolicyService.Api` | `Commands.Dtos` | `PersonDto`, `AddressDto`, `QuestionAnswer`, `TextQuestionAnswer`, `NumericQuestionAnswer`, `ChoiceQuestionAnswer`, `QuestionType` |
| `PolicyService.Api` | `Queries` | `GetPolicyDetailsQuery`, `GetPolicyDetailsQueryResult` |
| `PolicyService.Api` | `Queries.Dtos` | `PolicyDetailsDto` |
| `ProductService.Api` | `Queries` | `FindAllProductsQuery`, `FindProductByCodeQuery` |
| `ProductService.Api` | `Queries.Dtos` | `ProductDto`, `CoverDto`, `QuestionDto`, `ChoiceQuestionDto`, `DateQuestionDto`, `NumericQuestionDto`, `ChoiceDto`, `QuestionType` |

Not done yet: JSON converters for the polymorphic `QuestionDto` and `QuestionAnswer` types. They are needed at run time, not for the build, and belong to the service implementation.

## 3. Docker: `openjdk:8-jre-slim: not found`

- Error: `docker compose -f scripts/infra.yml up -d --build` failed while building `eureka-server`:
  `failed to resolve source metadata for docker.io/library/openjdk:8-jre-slim: not found`.
- Cause: the `openjdk:8-jre-slim` tag is no longer available on Docker Hub.
- Fix (instructor code changed): `eureka-server/Dockerfile`, line 6, `FROM openjdk:8-jre-slim` → `FROM eclipse-temurin:8-jre`.
- Result: the four containers start (`eureka-server`, `postgres`, `elasticsearch`, `rabbit`); `postgres` creates the databases from `postgres/createdatabases.sql` on first start.

## 4. Remaining warnings (not fixed)

All come from instructor code, which was left unchanged:

- NU1901, NU1903: `Steeltoe.Discovery.Eureka` 3.2.7, referenced by `AgentPortalApiGateway`, has known vulnerabilities.
- CS8632, CS8669: nullable annotations in `BlazorWasmClient`, whose project file sets `<Nullable>disable</Nullable>`.
- CA2017: a logging call in `BlazorWasmClient` whose arguments do not match its message template.

## Other notes

- `Web` (Vue 2) and `eureka-server` (Java) are not part of `dotnet build`. `eureka-server` is built by Docker through `scripts/infra.yml`.
- `db-backups/` holds one plain SQL dump (`pg_dump -C`) and one custom-format dump (`pg_dump -Fc`) per database, plus `roles.sql` (`pg_dumpall -r`).
