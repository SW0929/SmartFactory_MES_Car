# MES_SW
자동차 제조업 MES 프로젝트

# 📦 Smart Factory MES System

이 프로젝트는 제조 공장의 스마트 MES 시스템 구축을 위한 데이터베이스 모델입니다.

---

## 🗂️ ERD (Entity Relationship Diagram)

![MES_DB다이어그램](https://github.com/user-attachments/assets/65c66729-db35-4005-bada-a22322f324e3)


---

## 📑 테이블 설명

### 🔧 Equipment
| 컬럼명         | 설명                 |
|----------------|----------------------|
| EquipmentID    | 설비 고유 ID         |
| Name           | 설비 이름            |
| Type           | 설비 종류            |
| Status         | 설비 상태 (대기, 점검 등) |
| ProcessID      | 연결된 공정 ID       |
| LastUsedTime   | 마지막 사용 시간      |

---

### 🛠️ EquipmentDefect
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

### 🏭 Process
| 컬럼명     | 설명       |
|------------|------------|
| ProcessID  | 공정 ID    |
| Name       | 공정 이름  |
| Sequence   | 공정 순서  |
| Description| 설명       |

---

### 🧑‍🏭 Users
| 컬럼명      | 설명              |
|-------------|-------------------|
| EmployeeID  | 직원 ID           |
| UserName    | 사용자 이름        |
| UserRole    | 역할 (관리자, 작업자 등) |
| UserStatus  | 상태              |
| Department  | 부서              |

---

### 📝 WorkOrders
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

### 🔄 WorkOrderProcess
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

### 📦 Product
| 컬럼명   | 설명         |
|----------|--------------|
| ProductID| 제품 ID      |
| Name     | 제품 이름     |
| Model    | 제품 모델명   |
| Description | 설명     |

---

## 📎 참고 사항
- 이 구조는 기본적인 스마트팩토리 MES 흐름을 기반으로 설계되었습니다.
- 사용자는 역할(UserRole)에 따라 권한이 구분됩니다 (예: 관리자, 작업자).
- 설비 상태는 EquipmentDefect 테이블을 통해 점검/대기 전환됩니다.

---
