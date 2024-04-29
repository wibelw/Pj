package com.example.compartirgastos.data

import com.example.compartirgastos.model.Gasto
import com.example.compartirgastos.model.Person
import kotlinx.coroutines.flow.Flow
import org.mongodb.kbson.ObjectId

interface MongoRepository {
    fun configureTheRealm()
    fun getData(): Flow<List<Person>>
    fun filterData(name: String): Flow<List<Person>>
    suspend fun insertPerson(person: Person)
    suspend fun updatePerson(person: Person)
    suspend fun deletePerson(id: ObjectId)

    suspend fun insertGasto(gasto: Gasto)
    fun getGastos(): Flow<List<Gasto>>

    suspend fun deleteGasto(id:ObjectId)

    fun filterGasto(name: String): Flow<List<Gasto>>

    suspend fun updateGasto(gasto: Gasto)
}