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
## 📋 주요 기능

### ✅ 작업 지시 관리
- 작업 등록, 수정, 삭제
- 제품 선택, 수량 및 시작일 입력
- 설비 자동 할당 기능

### 🧰 설비 관리
- 설비 등록, 수정, 삭제
- 설비 상태 확인 ('대기', '가동', '고장', '정비 중')
- 결함 등록 및 해결 처리

### 📊 작업 실적 등록
- 공정별 양품/불량 실적 등록
- 실적 조회 및 수정

### 📈 공정별 실적 차트
- 공정별 '양품/불량' 수량 차트 시각화
- 실시간 생산 품질 현황 확인 가능

---

## 📑 DB

<details>
    <summary>🗂️ ERD (Entity Relationship Diagram)</summary>

![MES_DB다이어그램](https://github.com/user-attachments/assets/65c66729-db35-4005-bada-a22322f324e3)

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
---

## 🧭 프로젝트 구조
<details>
    <summary>🧭 프로젝트 구조</summary>



</details>

---

## 📎 참고 사항
- 이 구조는 기본적인 스마트팩토리 MES 흐름을 기반으로 설계되었습니다.
- 사용자는 역할(UserRole)에 따라 권한이 구분됩니다 (예: 관리자, 작업자).
- 설비 상태는 EquipmentDefect 테이블을 통해 점검/대기 전환됩니다.

---
