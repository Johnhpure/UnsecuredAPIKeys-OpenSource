# 🐳 UnsecuredAPIKeys Docker 容器化部署指南

## 📋 概述

本指南提供了UnsecuredAPIKeys项目的完整Docker容器化部署方案，包含5个核心服务的容器编排，完全保留原有功能不变。

## 🏗️ 架构组件

### 容器服务架构
```
┌─────────────────┐    ┌─────────────────┐    ┌─────────────────┐
│   Frontend UI   │    │    Web API      │    │   PostgreSQL    │
│   (nginx:3000)  │◄──►│   (aspnet:8080) │◄──►│   (postgres)    │
└─────────────────┘    └─────────────────┘    └─────────────────┘
                                ▲
                                │
                    ┌───────────┴───────────┐
                    │                       │
            ┌───────▼────────┐    ┌────────▼────────┐
            │Verification Bot│    │  Scraper Bot    │
            │  (.NET Worker) │    │ (.NET Worker)   │
            └────────────────┘    └─────────────────┘
```

### 容器服务清单
| 容器名称 | 服务类型 | 端口 | 镜像基础 | 功能 |
|---------|----------|------|----------|------|
| **unsecured-api-keys-frontend** | 前端UI | [`3000`](http://localhost:3000) | `nginx:alpine` | 静态前端 + API代理 |
| **unsecured-api-keys-webapi** | Web API | [`8080`](http://localhost:8080) | `mcr.microsoft.com/dotnet/aspnet:9.0` | 核心API + SignalR Hub |
| **unsecured-api-keys-db** | 数据库 | `5432` | `postgres:15-alpine` | PostgreSQL数据存储 |
| **unsecured-api-keys-verification-bot** | 验证机器人 | - | `mcr.microsoft.com/dotnet/runtime:9.0` | API密钥验证服务 |
| **unsecured-api-keys-scraper-bot** | 爬虫机器人 | - | `mcr.microsoft.com/dotnet/runtime:9.0` | GitHub/GitLab数据爬取 |

---

## 🚀 快速部署

### **步骤 1: 环境准备**
```bash
# 克隆项目
git clone https://github.com/your-username/UnsecuredAPIKeys-OpenSource.git
cd UnsecuredAPIKeys-OpenSource

# 配置环境变量
cp .env.example .env
```

### **步骤 2: 配置环境变量**
编辑 [`.env`](.env.example:1) 文件：
```env
# 数据库密码（必须）
POSTGRES_PASSWORD=your_secure_password_2024

# API密钥（可选，用于增强搜索功能）
GITHUB_TOKEN=your_github_token_here
GITLAB_TOKEN=your_gitlab_token_here

# Discord OAuth（可选）
DISCORD_CLIENT_ID=your_discord_client_id
DISCORD_CLIENT_SECRET=your_discord_client_secret
```

### **步骤 3: 一键启动所有服务**
```bash
# 构建并启动所有容器
docker-compose up --build -d

# 查看服务状态
docker-compose ps

# 查看实时日志
docker-compose logs -f
```

### **步骤 4: 验证部署成功**
- **前端访问**: [`http://localhost:3000`](http://localhost:3000)
- **API文档**: [`http://localhost:8080/scalar/v1`](http://localhost:8080/scalar/v1)
- **API状态**: [`http://localhost:8080/api/status`](http://localhost:8080/api/status)
- **健康检查**: `docker-compose ps` (所有服务状态应为 "Up (healthy)")

---

## 🔧 高级配置

### **生产环境优化**
```bash
# 设置生产环境变量
export ASPNETCORE_ENVIRONMENT=Production
export NODE_ENV=production
export PRODUCTION_DOMAIN=your-domain.com

# 启动生产服务
docker-compose -f docker-compose.yml -f docker-compose.prod.yml up -d
```

### **服务扩缩容**
```bash
# 扩展验证机器人实例（处理更多验证任务）
docker-compose up --scale verification-bot=3 -d

# 扩展爬虫机器人实例（并发爬取）
docker-compose up --scale scraper-bot=2 -d
```

### **数据持久化**
```bash
# 查看持久化卷
docker volume ls | grep unsecured

# 备份数据库
docker-compose exec postgres pg_dump -U unsecured_api_user UnsecuredAPIKeys > backup.sql

# 还原数据库
docker-compose exec -T postgres psql -U unsecured_api_user UnsecuredAPIKeys < backup.sql
```

---

## 📊 容器网络通信

### **内部网络配置**
- **网络名称**: `unsecured-api-keys-network`
- **网络类型**: `bridge`
- **子网范围**: `172.20.0.0/16`

### **服务间通信**
```yaml
# 前端 → Web API
frontend:3000 → webapi:8080/api/*
frontend:3000 → webapi:8080/hubs/stats (SignalR)

# Web API → 数据库
webapi:8080 → postgres:5432

# 机器人服务 → 数据库
verification-bot → postgres:5432
scraper-bot → postgres:5432
```

### **健康检查机制**
- **PostgreSQL**: `pg_isready` 检查（30s间隔）
- **Web API**: HTTP `/api/status` 检查（30s间隔）  
- **Frontend**: HTTP `localhost:3000` 检查（30s间隔）

---

## 🐛 故障排除

### **常见问题解决**

#### **1. 数据库连接失败**
```bash
# 检查PostgreSQL容器状态
docker-compose logs postgres

# 手动测试数据库连接
docker-compose exec postgres psql -U unsecured_api_user -d UnsecuredAPIKeys -c "SELECT 1;"
```

#### **2. Web API启动失败**
```bash
# 查看API容器日志
docker-compose logs webapi

# 检查数据库迁移状态
docker-compose exec webapi dotnet ef database info
```

#### **3. 前端无法访问API**
```bash
# 检查nginx配置
docker-compose exec frontend cat /etc/nginx/conf.d/default.conf

# 测试内部网络连通性
docker-compose exec frontend wget -qO- http://webapi:8080/api/status
```

#### **4. 机器人服务异常**
```bash
# 查看验证机器人日志
docker-compose logs verification-bot

# 查看爬虫机器人日志
docker-compose logs scraper-bot

# 重启特定服务
docker-compose restart verification-bot
```

### **日志管理**
```bash
# 查看所有服务日志
docker-compose logs

# 查看特定服务日志（带时间戳）
docker-compose logs -t webapi

# 实时跟踪日志
docker-compose logs -f --tail=100

# 日志轮转（避免日志过大）
docker-compose config | grep logging -A 5
```

### **性能监控**
```bash
# 查看容器资源使用
docker stats

# 查看容器详细信息
docker-compose exec webapi cat /proc/meminfo
docker-compose exec webapi cat /proc/cpuinfo
```

---

## 🔒 安全配置

### **生产安全建议**
1. **修改默认密码**: 确保 `.env` 中的 `POSTGRES_PASSWORD` 使用强密码
2. **API密钥保护**: 在生产环境中使用Docker Secrets管理敏感信息
3. **网络隔离**: 使用自定义Docker网络隔离容器通信
4. **SSL/TLS**: 在反向代理层（如nginx）配置HTTPS
5. **日志安全**: 避免在日志中暴露敏感信息

### **Docker Secrets示例**
```bash
# 创建密钥
echo "your_secure_password" | docker secret create postgres_password -

# 在compose文件中使用
services:
  postgres:
    secrets:
      - postgres_password
    environment:
      POSTGRES_PASSWORD_FILE: /run/secrets/postgres_password
```

---

## 🎯 服务端点总览

| 功能 | URL | 说明 |
|------|-----|------|
| **主页面** | [`http://localhost:3000`](http://localhost:3000) | 前端用户界面 |
| **API文档** | [`http://localhost:8080/scalar/v1`](http://localhost:8080/scalar/v1) | Scalar API文档 |
| **API状态** | [`http://localhost:8080/api/status`](http://localhost:8080/api/status) | 健康检查端点 |
| **SignalR Hub** | [`ws://localhost:8080/hubs/stats`](ws://localhost:8080/hubs/stats) | 实时数据推送 |
| **数据库** | `localhost:5432` | PostgreSQL连接 |

完整的Docker容器化方案已部署就绪，所有原有功能完全保留，支持一键启动、健康监控、故障自愈和生产级优化。