# 🏭 SmartFactory MES 프로젝트

자동차 제조업의 주요 공정(프레스, 차체, 도장, 의장, 검사)을 반영하여 구축된 제조 실행 시스템(MES) 입니다.

생산 현장의 작업 지시, 설비 관리, 실적 등록, 결함 처리 기능 등을 통합한 데스크탑 애플리케이션입니다.

---

## 🛠️ 사용 기술

- **Language**: C#
- **Framework**: .NET 8.0 + Windows Forms (WinForms)
- **Database**: Microsoft SQL Server (MSSQL)
- **Data Access**: ADO.NET (SqlCommand 기반 직접 구현)
- **Architecture**: MVC 유사 구조 (Form - Service - Repository - Model)

---


## 📋📸 주요 기능 및 UI (수정 중)

<details>
    <summary>🔐 로그인</summary>

![image](https://github.com/user-attachments/assets/1d3eef97-3e03-489d-b334-483cb216c105)


### ✨ 기능 요약
- 사번 기반 로그인
- 로그인 시 역할(관리자/작업자)에 따른 화면 분리
- 로그아웃 시 로그인 화면으로 복귀
</details>

<details>
    <summary>🛠️ 관리자</summary>

![image](https://github.com/user-attachments/assets/8a50ddf8-22a6-4502-820b-f04c27f0ac1c)

### 👤 사용자 관리
- 사용자 등록, 수정, 삭제
- 사용자 활성화 상태 변경
- 역할(관리자/작업자) 지정

### 📝 작업 지시 관리
- 작업 등록, 수정, 삭제
- 제품 선택, 수량/시작일 설정
- 설비 자동 할당
- 작업 상태 조회

### 🏭 설비 관리
- 설비 등록, 수정, 삭제
- 실시간 설비 상태 확인 (대기/가동/고장/점검)

### ⚙️ 결함 관리
- 작업자가 등록한 설비 결함 확인 및 처리
- 결함 사유 확인 가능

### 📊 공정별 실적 차트 (개발 중)
- 공정별 양품/불량 수량 시각화
- 실시간 생산 현황 모니터링 (향후 지원 예정)

</details>

<details>
    <summary>👷 작업자</summary>
    
![image](https://github.com/user-attachments/assets/534a89f0-f105-45c4-a5f0-f439c272cae6)
![image](https://github.com/user-attachments/assets/db65e844-f2f4-4d4d-94b7-1320489ec791)


### 📝 작업 지시
- 공정별 할당 작업 목록 확인
- 작업 시작/완료 처리
- 날짜별 작업 필터링

### 📈 작업 실적 등록
- 양품/불량 수량 입력
- 완료 작업 더블 클릭 시 실적 입력 폼 팝업
- 실적 조회 및 수정 가능

### ⚠️ 설비 결함 등록
- 현재 가동 중인 설비 목록 표시
- 이상 발생 시 설비 결함 등록 가능

### 공정별 실적 차트 (개발 중)
- 양품/불량 수량 시각화
- 실시간 생산 흐름 확인 가능

</details>





---

## 📑 DB

<details>
    <summary>🗂️ ERD (Entity Relationship Diagram)</summary>

![image](https://github.com/user-attachments/assets/34387e89-437c-43b2-a108-0c74c673215c)


</details>

<details>
    <summary>📑 테이블 설명</summary>

### 🧑‍🏭 Users (사용자 테이블)
| 컬럼명      | 설명              |
|-------------|-------------------|
| EmployeeID  | 직원 ID           |
| UserName    | 사용자 이름        |
| UserRole    | 역할 (관리자, 작업자 등) |
| UserStatus  | 상태              |
| Department  | 부서              |

---

### 🏭 Process (공정 테이블)
| 컬럼명     | 설명       |
|------------|------------|
| ProcessID  | 공정 ID    |
| Name       | 공정 이름  |
| Sequence   | 공정 순서  |
| Description| 설명       |

---

### 📦 Product (제품 테이블)
| 컬럼명   | 설명         |
|----------|--------------|
| ProductID| 제품 ID      |
| Name     | 제품 이름     |
| Model    | 제품 모델명   |
| Description | 설명     |

---

### 🔧 Equipment (설비 테이블)
| 컬럼명         | 설명                 |
|----------------|----------------------|
| EquipmentID    | 설비 고유 ID         |
| Name           | 설비 이름            |
| Type           | 설비 종류            |
| Status         | 설비 상태 (대기, 점검 등) |
| ProcessID      | 연결된 공정 ID       |
| LastUsedTime   | 마지막 사용 시간      |

---

### 🛠️ EquipmentDefect (설비 결함 테이블)
| 컬럼명         | 설명                   |
|----------------|------------------------|
| DefectID       | 고장 보고 ID            |
| EquipmentID    | 관련 설비 ID            |
| DefectTime     | 고장 발생 시간          |
| ReportedBy     | 보고한 작업자 ID        |
| DefectType     | 고장 유형               |
| Description    | 상세 설명               |
| Resolved       | 해결 여부 (0 또는 1)    |
| ResolvedTime   | 해결된 시간             |

---

### 📝 WorkOrders (작업 지시 테이블)
| 컬럼명     | 설명               |
|------------|--------------------|
| WorkOrderID| 작업지시서 ID       |
| ProductID  | 제품 ID            |
| OrderQty   | 주문 수량          |
| StartDate  | 시작일자           |
| Department | 지시 부서          |
| IssueBy    | 지시자 ID          |
| Status     | 상태 (진행 중 등)   |

---

### 🔄 WorkOrderProcess (작업 지시의 공정 흐름 테이블)
| 컬럼명         | 설명                   |
|----------------|------------------------|
| WorkOrderProcessID | 고유 ID             |
| WorkOrderID    | 연결된 작업지시서 ID   |
| ProcessID      | 공정 ID                |
| EquipmentID    | 사용 설비 ID           |
| AssignedUserID | 담당 작업자 ID         |
| Status         | 진행 상태              |
| StartTime      | 시작 시간              |
| EndTime        | 종료 시간              |

---

### 🔄 WorkOrderProcessLog (작업 지시 로그 테이블)
| 컬럼명         | 설명                   |
|----------------|------------------------|
| LogID          | 로그 고유 ID           |
| WorkOrderProcessID        | 작업지시 흐름 ID            |
| WorkOrderID      | 작업지시 ID                |
| ProcessID      | 공정 ID                |
| EquipmentID    | 사용 설비 ID           |
| AssignedUserID   | 관리자 ID              |
| StartTime      | 작업 시작일                 |
| EndTime         | 작업 종료일              |
| LoggedAt        | 로그 기록 날짜         | 

---

### 🔄 WorkPerformance (실적 테이블)
| 컬럼명         | 설명                   |
|----------------|------------------------|
| PerformanceID  | 실적고유 ID            |
| OrderUD        | 작업지시 ID            |
| ProcessID      | 공정 ID                |
| ProductID      | 제품 ID                |
| RegisteredBy   | 작업자 ID              |
| EquipmentID    | 사용 설비 ID           |
| GoodQty        | 양품                   |
| DefectQty      | 불량품                 |
| Reason         | 불량 사유              |
| RegDate        | 실적 등록 날짜         | 
| UpdateDate     | 실적 수정 날짜         |

</details>


## 🧭 프로젝트 구조
<details>
    <summary> 프로젝트 구조 </summary></summary>

```bash
MES_SW
├── Admin                    # 관리자 관련 기능
│   ├── AdminUserControl     # 관리자 화면(UserControls)
│   │   ├── UserControl_Dashboard.cs
│   │   ├── UserControl_Equipment.cs
│   │   ├── UserControl_EquipmentDefect.cs
│   │   ├── UserControl_UserManager.cs
│   │   └── UserControl_WorkOrder.cs
│   ├── Forms                # 관리자 메인 폼
│   │   └── AdminForm.cs
│   └── Models               # 관리자 전용 모델
│       ├── Employee.cs
│       ├── WorkOrder.cs
│       └── Items/           # 드롭다운, 리스트용 모델
│           ├── DepartmentItem.cs
│           ├── EquipmentItem.cs
│           ├── ProcessItem.cs
│           └── ProductItem.cs

├── Worker                  # 작업자 관련 기능
│   ├── Forms                # 작업자 메인/서브 폼
│   │   ├── WorkerForm.cs
│   │   └── WorkPerformanceForm.cs
│   ├── Models               # 작업자용 데이터 모델
│   │   ├── WorkOrder.cs
│   │   └── WorkOrderPerformance.cs
│   └── WorkerUserControl    # 작업자 화면(UserControls)
│       ├── UserControl_EquipmentList.cs
│       ├── UserControl_WorkOrderCard.cs
│       ├── UserControl_WorkOrderList.cs
│       └── UserControl_WorkPerformance.cs

├── Services                # 비즈니스 로직 계층
│   ├── Admin
│   │   ├── EquipmentDefectService.cs
│   │   ├── UserManageService.cs
│   │   └── WorkOrderService.cs
│   ├── Common               # 공통 서비스
│   │   ├── EquipmentService.cs
│   │   ├── ProcessService.cs
│   │   └── ProductService.cs
│   └── Worker
│       ├── WorkOrderServices.cs
│       └── WorkPerformanceService.cs

├── Data                   # DB 액세스 계층 (Repository 패턴)
│   ├── DBHelper.cs         # 공통 DB 유틸리티
│   ├── EquipmentDefect.cs
│   ├── EquipmentRepository.cs
│   ├── ProcessRepository.cs
│   ├── ProductRepository.cs
│   ├── UserRepository.cs
│   ├── Admin
│   │   ├── EquipmentDefectRepository.cs
│   │   ├── UserManageRepository.cs
│   │   └── WorkOrderRepository.cs
│   └── Worker
│       ├── WorkOrderPerformanceRepository.cs
│       └── WorkOrderRepository.cs

├── Login                  # 로그인 폼
│   └── LoginForm.cs
```
</details>

<details>
    <summary> 시스템 구조 </summary></summary>

```bash
[WinForms UI] 
    ↓
[Service Layer]  ← 유효성 검증, 트랜잭션 관리
    ↓
[Repository Layer] ← SQL 실행, DB 접근
    ↓
[SQL Server (MSSQL)]
```
</details>

---

## 📎 참고 사항
- 이 구조는 기본적인 스마트팩토리 MES 흐름을 기반으로 설계되었습니다.
- 사용자는 역할(UserRole)에 따라 권한이 구분됩니다 (예: 관리자, 작업자).
- 자세한 코드 설명은 블로그에 올리겠습니다.

---

## 📦 향후 개선 사항

- [ ] UI/UX 개선
- [ ] REST API 제공 및 웹 버전 연동 (ASP.NET Core + Vue.js)
- [ ] 설비 실시간 연동 (MQTT 사용)
- [ ] 설비 가동률, 공정별 효율 분석 기능 추가
- [ ] 자재 관리 기능 추가

---

## 깨달은 것
- 초기 계획 및 설계를 제대로 해야 한다.
- 메서드 하나에는 하나의 기능만 넣고 메서드 이름은 결과 값이 예상 가는 이름으로 하자!
