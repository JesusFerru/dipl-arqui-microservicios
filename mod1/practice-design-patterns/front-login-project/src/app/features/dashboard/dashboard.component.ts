import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { AuthService } from '../../core/auth/auth.service';
import { LoggerService, LogEntry } from '../../core/logging/logger.service';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule],
  template: `
    <div class="min-h-screen bg-gray-50 flex flex-col">
      <nav class="bg-white shadow-md p-4">
        <div class="max-w-7xl mx-auto flex justify-between items-center">
          <h1 class="text-2xl font-bold text-gray-800">Dashboard</h1>
          <button 
            (click)="logout()"
            class="bg-red-500 hover:bg-red-600 text-white px-4 py-2 rounded transition duration-200">
            Cerrar Sesión
          </button>
        </div>
      </nav>

      <main class="flex-grow max-w-7xl mx-auto w-full p-6">
        <div class="bg-white p-6 mb-4 rounded border border-gray-200">
          <h2 class="text-2xl text-gray-800 mb-2">¡Bienvenido!</h2>
          <p class="text-gray-600">Has iniciado sesión correctamente.</p>
        </div>

        <div class="bg-white rounded border border-gray-200 overflow-hidden">
          <div class="px-6 py-4 border-b border-gray-200 flex justify-between items-center">
            <h3 class="text-lg text-gray-900">Registro de Actividad</h3>
            <div>
              <button (click)="downloadLogs()" class="text-sm bg-gray-200 text-gray-800 px-3 py-1 rounded mr-2 hover:bg-gray-300">Descargar archivolog.txt</button>
              <button (click)="refreshLogs()" class="text-sm text-blue-600 hover:underline">Actualizar</button>
            </div>
          </div>
          <div class="overflow-x-auto">
            <table class="min-w-full divide-y divide-gray-200">
              <thead class="bg-gray-50">
                <tr>
                  <th class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Fecha y Hora</th>
                  <th class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Evento</th>
                </tr>
              </thead>
              <tbody class="bg-white divide-y divide-gray-200">
                <tr *ngFor="let log of logs">
                  <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-500">{{ log.timestamp | date:'medium' }}</td>
                  <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-900">{{ log.message }}</td>
                </tr>
                <tr *ngIf="logs.length === 0">
                  <td colspan="2" class="px-6 py-4 text-center text-gray-500">No hay registros disponibles.</td>
                </tr>
              </tbody>
            </table>
          </div>
        </div>
      </main>
    </div>
  `
})
export class DashboardComponent implements OnInit {
  logs: LogEntry[] = [];

  constructor(
    private authService: AuthService,
    private loggerService: LoggerService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.refreshLogs();
  }

  refreshLogs(): void {
    this.logs = this.loggerService.getLogs().reverse();
  }

  downloadLogs(): void {
    const logsStr = this.logs.map(log => `[${new Date(log.timestamp).toLocaleString()}] ${log.message}`).join('\n');
    const blob = new Blob([logsStr], { type: 'text/plain' });
    const url = window.URL.createObjectURL(blob);
    const a = document.createElement('a');
    a.href = url;
    a.download = 'archivolog.txt';
    a.click();
    window.URL.revokeObjectURL(url);
  }

  logout(): void {
    this.authService.logout();
    this.router.navigate(['/home']);
  }
}
