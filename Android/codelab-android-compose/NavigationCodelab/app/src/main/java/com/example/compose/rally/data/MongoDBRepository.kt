package com.example.compose.rally.data

import io.realm.kotlin.Realm
import io.realm.kotlin.RealmConfiguration

class MongoDBRepository {
    fun TestWrite(){
       // val config = RealmConfiguration.create(schema = setOf(Item::class))
        val config = RealmConfiguration.Builder(
            schema = setOf(Item::class)
        )
            .inMemory()
            .build()

        val realm: Realm = Realm.open(config)
        realm.writeBlocking {
            copyToRealm(Item().apply {
                summary = "Do the laundry"
                isComplete = false
            })
        }
    }
}