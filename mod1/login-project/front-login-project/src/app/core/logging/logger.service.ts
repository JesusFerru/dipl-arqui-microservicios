import { Injectable } from '@angular/core';

export interface LogEntry {
  timestamp: string;
  message: string;
}

@Injectable({
  providedIn: 'root'
})
export class LoggerService {
  private readonly LOG_KEY = 'app_logs';

  constructor() {
    if (!localStorage.getItem(this.LOG_KEY)) {
      localStorage.setItem(this.LOG_KEY, JSON.stringify([]));
    }
  }

  log(message: string): void {
    const logs = this.getLogs();
    logs.push({
      timestamp: new Date().toISOString(),
      message
    });
    localStorage.setItem(this.LOG_KEY, JSON.stringify(logs));
  }

  getLogs(): LogEntry[] {
    const logsStr = localStorage.getItem(this.LOG_KEY);
    return logsStr ? JSON.parse(logsStr) : [];
  }
  
  clearLogs(): void {
    localStorage.removeItem(this.LOG_KEY);
  }
}
