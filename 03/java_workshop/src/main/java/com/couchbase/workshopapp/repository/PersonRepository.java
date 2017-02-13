package com.couchbase.workshopapp.repository;

import com.couchbase.workshopapp.entity.Person;
import org.springframework.data.couchbase.core.query.Query;
import org.springframework.data.repository.CrudRepository;
import org.springframework.stereotype.Repository;

/**
 * Person repository extends from the crud repository provided by spring-data-couchbase.
 */
@Repository
public interface PersonRepository extends CrudRepository<Person, String> {
    /**
     * Query derivation for findallPeople in our person respository using spel
     * n1ql.selectEntity is a predefined select clause to fetch all properties of the person entity
     * n1ql.filter adds a where criteria to match entities in our repository (under the hoods it is _class
     * attribute to track entity information which gets stored in the couchbase document)
     */
    @Query("#{#n1ql.selectEntity} WHERE #{#n1ql.filter}")
    Iterable<Person> findAllPeople();
}