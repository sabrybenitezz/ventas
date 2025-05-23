﻿using ConcesionariaVehiculos.Data;
using ConcesionariaVehiculos.Models;
using Microsoft.EntityFrameworkCore;



public class ClienteRepository : IClienteRepository
{
    private readonly AppDbContext _context;

    public ClienteRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Cliente>> GetAllAsync()
    {
        return await _context.Clientes.ToListAsync();
    }

    public async Task<Cliente?> GetByIdAsync(int id)
    {
        return await _context.Clientes.FindAsync(id);
    }

    public async Task AddAsync(Cliente cliente)
    {
        await _context.Clientes.AddAsync(cliente);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Cliente cliente)
    {
        var existingCliente = await _context.Clientes.AsNoTracking().FirstOrDefaultAsync(p => p.Id == cliente.Id);

        if (existingCliente == null)
        {
            throw new KeyNotFoundException("El cliente no existe.");
        }

        // Attach the updated entity and set its state to Modified
        _context.Attach(cliente);
        _context.Entry(cliente).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }


    public async Task DeleteAsync(int id)
    {
        var cliente = await _context.Clientes.FindAsync(id);
        if (cliente != null)
        {
            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();
        }
    }
}